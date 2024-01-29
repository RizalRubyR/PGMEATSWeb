using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsSportMaster
    {
        public string SportID { get; set; }
        public string SportDesc { get; set; }
        public string ActiveStatus { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
        public string FileName { get; set; }
        public string files { get; set; }
        public string FileName2 { get; set; }
        public string files2 { get; set; }
    }

    public class clsSportMasterDB
    {
        public clsResponse SportMasterList()
        {
            List<clsSportMaster> Menus = new List<clsSportMaster>();
           clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SportMaster_GetList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsSportMaster Menu = new clsSportMaster();
                        Menu.SportID = rd["SportID"].ToString();
                        Menu.SportDesc = rd["SportDesc"].ToString();
                        Menu.ActiveStatus = rd["ActiveStatus"].ToString();
                        Menu.LastUser = rd["UpdateUser"].ToString();
                        Menu.LastUpdate = rd["UpdateDate"].ToString();
                        Menu.FileName = rd["FileName"].ToString();
                        Menus.Add(Menu);
                    }

                    Response.Message = "Success";
                    Response.Contents = Menus;                
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
                
            }
            return Response;
        }

        public clsResponse SportMasterIns(clsSportMaster dataFrom)
        {
            clsResponse Response = new clsResponse();
            clsSportMaster data = new clsSportMaster();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SportMaster_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SportID", dataFrom.SportID ?? "");
                    cmd.Parameters.AddWithValue("SportDescription", dataFrom.SportDesc ?? "");
                    cmd.Parameters.AddWithValue("ActiveStatus", dataFrom.ActiveStatus ?? "");
                    cmd.Parameters.AddWithValue("UserID", dataFrom.LastUser ?? "");
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Response.Contents = rd["SportID"].ToString();
                    }

                    Response.Message = "Successfully Saved data!";

                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }

            return Response;
        }

        public clsResponse SportMasterUpd(clsSportMaster dataFrom, string TypeUpdate)
        {
            clsResponse Response = new clsResponse();
            clsSportMaster data = new clsSportMaster();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SportMaster_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SportID", dataFrom.SportID ?? "");
                    if(TypeUpdate == "1")
                    {
                        cmd.Parameters.AddWithValue("SportDescription", dataFrom.SportDesc ?? "");
                        cmd.Parameters.AddWithValue("ActiveStatus", dataFrom.ActiveStatus ?? "");
                        cmd.Parameters.AddWithValue("FileName", dataFrom.FileName ?? "");
                        cmd.Parameters.AddWithValue("TypeUpdate", TypeUpdate);
                    }
                    else if(TypeUpdate == "2")
                    {
                        cmd.Parameters.AddWithValue("FileName2", dataFrom.FileName2 ?? "");
                        cmd.Parameters.AddWithValue("TypeUpdate", TypeUpdate);
                    }
                    cmd.Parameters.AddWithValue("UserID", dataFrom.LastUser ?? "");
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Response.Contents = rd["SportID"].ToString();
                    }

                    Response.Message = "Successfully updated data!";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }

            return Response;
        }

        public clsResponse SportMasterDel(string SportID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SportMaster_Del", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SportID", SportID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    Response.Message = dt.Rows[0]["Msg"].ToString();
          
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }

            return Response;
        }

        public clsResponse FillCombo(string Type)
        {
            clsResponse Response = new clsResponse();
            List<clsFillCombo> fillcombos = new List<clsFillCombo>();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_FilterCombo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Type", Type);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsFillCombo fillcombo = new clsFillCombo();
                        fillcombo.Code = rd["Code"].ToString();
                        fillcombo.Description = rd["Description"].ToString();
                        fillcombos.Add(fillcombo);
                    }
                    Response.Message = "Success";
                    Response.Contents = fillcombos;
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }

    }
}