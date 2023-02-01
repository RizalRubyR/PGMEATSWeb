using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsIssueTypeMaster
    {
        public string IssueTypeID { get; set; }
        public string IssueTypeDesc { get; set; }
        public string ActiveStatus { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
        public string FileName { get; set; }
    }

    public class clsIssueTypeMaterDB
    {
        public clsResponse IssueTypeList(String IssueTypeID)
        {
            List<clsIssueTypeMaster> Menus = new List<clsIssueTypeMaster>();
           clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_IssueType_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IssueTypeID", IssueTypeID??"");
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsIssueTypeMaster Menu = new clsIssueTypeMaster();
                        Menu.IssueTypeID = rd["IssueTypeID"].ToString();
                        Menu.IssueTypeDesc = rd["IssueTypeDesc"].ToString();
                        Menu.ActiveStatus = rd["ActiveStatus"].ToString();
                        Menu.LastUser = rd["UpdateUser"].ToString();
                        Menu.LastUpdate = rd["UpdateDate"].ToString();
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

        public clsResponse IssueTypeUpd(string IssueTypeID, string IssueTypeDesc, string ActiveStatus, string User)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_IssueType_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IssueTypeID", IssueTypeID);
                    cmd.Parameters.AddWithValue("IssueTypeDesc", IssueTypeDesc);
                    cmd.Parameters.AddWithValue("ActiveStatus", ActiveStatus);
                    cmd.Parameters.AddWithValue("User", User);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    if (i == 0)
                    {
                        Response.Message = "Success";
                    }
                    else
                    {
                        Response.Message = "Error - Query Insert Issue Type!";
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }

            return Response;
        }

        public clsResponse IssueTypeDel(string IssueTypeID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_IssueType_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IssueTypeID", IssueTypeID);
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

    }
}