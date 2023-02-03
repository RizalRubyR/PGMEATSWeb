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
        public string files { get; set; }
    }

    public class clsIssueTypeMaterDB
    {
        public clsResponse IssueTypeList()
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

        public clsResponse IssueTypeIns(clsIssueTypeMaster dataFrom)
        {
            clsResponse Response = new clsResponse();
            clsIssueTypeMaster data = new clsIssueTypeMaster();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_IssueType_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IssueTypeDesc", dataFrom.IssueTypeDesc ?? "");
                    cmd.Parameters.AddWithValue("ActiveStatus", dataFrom.ActiveStatus ?? "");
                    cmd.Parameters.AddWithValue("UserID", dataFrom.LastUser ?? "");
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Response.Contents = rd["IssueTypeID"].ToString();
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

        public clsResponse IssueTypeUpd(clsIssueTypeMaster dataFrom)
        {
            clsResponse Response = new clsResponse();
            clsIssueTypeMaster data = new clsIssueTypeMaster();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_IssueType_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IssueTypeID", dataFrom.IssueTypeID ?? "");
                    cmd.Parameters.AddWithValue("IssueTypeDesc", dataFrom.IssueTypeDesc ?? "");
                    cmd.Parameters.AddWithValue("ActiveStatus", dataFrom.ActiveStatus ?? "");
                    cmd.Parameters.AddWithValue("FileName", dataFrom.FileName ?? "");
                    cmd.Parameters.AddWithValue("UserID", dataFrom.LastUser ?? "");
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Response.Contents = rd["IssueTypeID"].ToString();
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

        public clsResponse IssueTypeDel(string IssueTypeID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_IssueType_Del", con);
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