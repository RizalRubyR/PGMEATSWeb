using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsResponseTemplate
    {
        public int ResponseID { get; set; }
        public string Description { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateDate { get; set; }
    }

    public class clsResponseTemplateDB
    {
        public clsResponse GetList()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            List<clsResponseTemplate> ResponseList = new List<clsResponseTemplate>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_ResponseTemplate_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    clsResponseTemplate Resp = new clsResponseTemplate();
                    Resp.ResponseID = Convert.ToInt16(rd["ResponseID"]);
                    Resp.Description = rd["Description"].ToString().Trim();
                    Resp.UpdateUser = rd["UpdateUser"].ToString().Trim();
                    Resp.UpdateDate = rd["UpdateDate"].ToString();
                    ResponseList.Add(Resp);
                }
                clsResponse Response = new clsResponse();
                Response.Message = "Success";
                Response.Contents = ResponseList;
                return Response;
            }
        }

        public clsResponse Insert(clsResponseTemplate data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ResponseTemplate_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Description", data.Description ?? "");
                    cmd.Parameters.AddWithValue("UpdateUser", data.UpdateUser  ?? "");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Message = "Insert data successful";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }

        public clsResponse Update(clsResponseTemplate data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ResponseTemplate_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ResponseID", data.ResponseID);
                    cmd.Parameters.AddWithValue("Description", data.Description ?? "");
                    cmd.Parameters.AddWithValue("UpdateUser", data.UpdateUser ?? "");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Message = "Update data successful";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }

        public clsResponse Delete(int ResponseID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ResponseTemplate_Del", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ResponseID", ResponseID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Message = "Delete data successful";
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