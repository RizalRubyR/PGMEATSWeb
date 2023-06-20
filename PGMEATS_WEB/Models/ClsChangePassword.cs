using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace PGMEATS_WEB.Models
{
    public class ClsChangePassword
    {
        public String Password { get; set; }
        public String NewPassword { get; set; }
        public String ConfPassword { get; set; }
        public String UserID { get; set; }


        public ClsChangePassword GetPassword(string UserID)
        {
            try
            {
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 2);
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    ClsChangePassword Pass = new ClsChangePassword();
                    while (rd.Read())
                    {
                        Pass.Password = encrypt.DecryptData(rd["Password"].ToString().Trim());
                    }

                    return Pass;
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public string ChangePassword(string UserID, string NewPassword)
        {
            List<JSOX> jsoxList = new List<JSOX>();
            Response clsrespon = new Response();
            Encryption encrypt = new Encryption();
            string Message = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //STARTING GET JSOX
                    string sql = "sp_JSOX_GET";
                    clsrespon = new Response();

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dtJsox = new DataTable();
                    SqlDataAdapter daJsox = new SqlDataAdapter(cmd);
                    daJsox.Fill(dtJsox);
                    daJsox.Dispose();
                    cmd.Dispose();
                    con.Close();

                    jsoxList = dtJsox.AsEnumerable().Select(x => new JSOX()
                    {
                        ID = Convert.ToInt16(x.Field<object>("RuleID")),
                        JsoxEnabled = Convert.ToBoolean(x.Field<object>("Enable")),
                        Desc = x.Field<string>("Description"),
                        Value = Convert.ToInt16(x.Field<object>("Value"))
                    }).ToList();
                    //ENDING GET JSOX

                    Message = ClsChangePassword.CheckingChangePassword(UserID, NewPassword, jsoxList);

                    if (Message == "")
                    {
                        sql = "sp_UserID_ChangedPassword";
                        cmd = new SqlCommand(sql, con);
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("UserID", UserID);
                        cmd.Parameters.AddWithValue("NewPassword", encrypt.EncryptData(NewPassword));
                        cmd.ExecuteNonQuery();
                        Message = "success";
                        con.Close();
                    };

                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Message;
        }

        public static string CheckingChangePassword(string staffID, string password, List<JSOX> ListJsox)
        {
            string msg = "";
            ClsChangePassword db = new ClsChangePassword();
            JSOX tmpjsox = new JSOX();
            DataTable dt = new DataTable();

            //Starts CHECK JSOX for Minimum Password
            tmpjsox = ListJsox.Find(x => x.ID == 1);

            if (tmpjsox.JsoxEnabled && password.Length < tmpjsox.Value)
            {
                msg = "Password length minimum " + tmpjsox.Value + " characters";
                return msg;
            }
            //Ending CHECK JSOX for Minimum Password

            //Starts CHECK JSOX for Contain StaffID
            tmpjsox = ListJsox.Find(x => x.ID == 7);

            if (tmpjsox.JsoxEnabled && password.Contains(staffID))
            {
                msg = "Password Cannot Contain Staff ID";
                return msg;
            }
            //Ending CHECK JSOX for Contain StaffID

            //Starts CHECK JSOX for Must contain 1 digit
            tmpjsox = ListJsox.Find(x => x.ID == 8);

            if (tmpjsox.JsoxEnabled)
            {
                string pattern = @"^(?=.*[a-zA-Z])(?=.*\d)";
                if (!System.Text.RegularExpressions.Regex.IsMatch(password, pattern))
                {
                    msg = tmpjsox.Desc;
                    return msg;
                }
            }
            //Ending CHECK JSOX for Must contain 1 digit

            //Starts CHECK JSOX for Cannot Repeating Characters/digit
            tmpjsox = ListJsox.Find(x => x.ID == 9);

            if (!tmpjsox.JsoxEnabled)
            {
                string PrevChar = password.Substring(0, 1);
                for (int i = 1; i < password.Length; i++)
                {
                    if (password.Substring(i, 1).ToUpper() == PrevChar.ToUpper())
                    {
                        msg = "Password Cannot Contain Repeating Characters";
                        return msg;
                    }
                    else
                    {
                        PrevChar = password.Substring(i, 1);
                    }
                }

                //string PrevChar = password.Substring(0, 1);
                //for (int i = 1; i < password.Length; i++)
                //{
                //    if (password.Remove(i-1, 1).Contains(PrevChar))
                //    {
                //        msg = "Password cannot contain repeating characters";
                //        return msg;
                //    }
                //    else
                //    {
                //        PrevChar = password.Substring(i, 1);
                //    }
                //}
            }
            //Ending CHECK JSOX for Cannot Repeating Characters/digit

            //Ada Update 13 May 2023 Jika Belum 90 Hari dari JSOXPassword History gusah ngecek JSOX ID = 5 yaa
            dt = db.UserGetLastPassword(staffID);
            if (msg == "")
            {
                if (dt.Rows.Count > 0)
                {
                    DateTime DateUpdate = Convert.ToDateTime(dt.Rows[0]["Updatedate"]);
                    DateTime DateServer = Convert.ToDateTime(dt.Rows[0]["DateServer"]);
                    int diff = (DateUpdate - DateServer).Days;

                    //Starts jSox for Expiry Password
                    tmpjsox = ListJsox.Find(x => x.ID == 3);
                    if (tmpjsox.JsoxEnabled)
                    {
                        if (diff >= tmpjsox.Value)
                        {
                            //Starts CHECK JSOX for Retain for x Times
                            tmpjsox = ListJsox.Find(x => x.ID == 5);

                            if (tmpjsox.JsoxEnabled)
                            {
                                dt = db.UserGetLastPasswordRetain(staffID, tmpjsox.Value);
                                if (dt.Select("Password = '" + password.ToLower() + "'").Length >= tmpjsox.Value)
                                {
                                    msg = "Password cannot be the same with your last " + tmpjsox.Value + " passwords";
                                    return msg;
                                }
                            }
                            //Ending CHECK JSOX for Retain for x Times
                        }
                    }
                    //Ending jSox for Expiry Password
                }
            }
            //Ending CHECK JSOX for Retain for x Times

            return msg;
        }

        public DataTable UserGetLastPassword(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string sql = "select top 1 Updatedate, DateServer = GETDATE() from M_JSOXPasswordHistory where UserID = @UserID And AppID = 'WEB' order by UpdateDate desc";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.Dispose();
                    con.Close();

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DataTable UserGetLastPasswordRetain(string UserID, int Value)
        {
            DataTable dt = new DataTable();
            Encryption encrypt = new Encryption();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string sql = "select top " + Value + " * from M_JSOXPasswordHistory where UserID = @StaffID And AppID = 'WEB' order by UpdateDate desc";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.Dispose();
                    con.Close();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["Password"] = encrypt.DecryptData(dt.Rows[i]["Password"].ToString());
                    }

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }

    public class JSOX
    {
        public int ID { get; set; }
        public string Desc { get; set; }
        public bool JsoxEnabled { get; set; }
        public int Value { get; set; }
        public string AppID { get; set; }
        public string UserID { get; set; }
        public string IpAddress { get; set; }
        public string HostName { get; set; }
        public string Activity { get; set; }
        public string Remark { get; set; }

    }
}