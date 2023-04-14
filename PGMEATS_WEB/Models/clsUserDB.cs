using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsUserDB
    {
        public clsUser GetUser(string UserID)
        {
            try
            {
                clsUser UserDat = new clsUser();
                Encryption encrypt = new Encryption();

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    //OldSP
                    //string sql = "usp_UserSetup_CheckUser";
                    
                    //New Stored Procedure using another sp get as refrence
                    string sql = "sp_UserSetup";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID.ToString());
                    cmd.Parameters.AddWithValue("Func", 2);
                    
                    //Old SP need to pass password, different type of Encrypt make stored procedure need to be differnt
                    //cmd.Parameters.AddWithValue("Password", encrypt.EncryptData(Password));
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        UserDat.UserID = sdr["UserID"].ToString();
                        UserDat.Password = encrypt.DecryptData(sdr["Password"].ToString());
                        UserDat.FullName = sdr["UserName"].ToString();
                    }

                    return UserDat;

                    //Old SP jsut need to return index ,but now need to return data 
                    //return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<clsUser> UserList(string UserID)
        {
            try
            {
                Encryption encrypt = new Encryption();

                List<clsUser> Users = new List<clsUser>();

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID.ToString());
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUser User = new clsUser();
                        User.UserID = rd["UserID"].ToString();
                        User.FullName = rd["FullName"].ToString();
                        User.Password = rd["Password"].ToString();
                        User.Password = encrypt.DecryptData(User.Password);
                        User.UserType = rd["UserType"].ToString();
                        User.GroupID = rd["GroupID"].ToString();
                        if (!Convert.IsDBNull(rd["LastUser"]))
                        {
                            User.LastUser = rd["LastUser"].ToString();
                        }
                        if (!Convert.IsDBNull(rd["LastUpdate"]))
                        {
                            User.LastUpdate = rd["LastUpdate"].ToString();
                        }

                        Users.Add(User);
                    }
                    return Users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<clsUserType> UserTypeList()
        {
            try
            {
                List<clsUserType> Users = new List<clsUserType>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_GetUserType";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserType User = new clsUserType();
                        User.UserTypeCode = rd["UserTypeCode"].ToString();
                        Users.Add(User);
                    }
                    return Users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<clsUserGroups> UserGroupList()
        {
            try
            {
                List<clsUserGroups> Users = new List<clsUserGroups>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_GetUserGroup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserGroups User = new clsUserGroups();
                        User.GroupID = rd["GroupID"].ToString();
                        User.GroupDescription = rd["GroupDescription"].ToString();
                        Users.Add(User);
                    }
                    return Users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(clsUser User, string UserLogin)
        {
            try
            {
                Encryption encrypt = new Encryption();

                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_InsUpd";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", User.UserID);
                    cmd.Parameters.AddWithValue("FullName", User.FullName);
                    cmd.Parameters.AddWithValue("Password", encrypt.EncryptData(User.Password));
                    cmd.Parameters.AddWithValue("GroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("UserType", User.UserType);
                    cmd.Parameters.AddWithValue("UserLogin", UserLogin);
                    con.Open();

                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(string UserID)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_Del";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    con.Open();

                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangePassword(clsChangePassword password)
        {
            try
            {
                Encryption encrypt = new Encryption();

                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_ChangePassword";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", password.UserID);
                    cmd.Parameters.AddWithValue("Password", encrypt.EncryptData(password.Password));
                    con.Open();

                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}