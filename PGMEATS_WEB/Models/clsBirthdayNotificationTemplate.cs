using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsBirthdayNotificationTemplate
    {
        public string BirthdayTemplateID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ImageData { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class clsBirthdayNotificationTemplateDB
    {
        public clsResponse GetBirthdayNotificationTemplateList()
        {
            List<clsBirthdayNotificationTemplate> BirthdayNotificationTemplateList = new List<clsBirthdayNotificationTemplate>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_GET_BirthdayNotificationTemplate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    BirthdayNotificationTemplateList = dt.AsEnumerable().Select(x =>
                    new clsBirthdayNotificationTemplate
                    {
                        BirthdayTemplateID = x.Field<Int32>("BirthdayTemplateID").ToString(),
                        Subject = x.Field<string>("Subject"),
                        Content = x.Field<string>("Content"),
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = BirthdayNotificationTemplateList;
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

        public clsResponse GetBirthdayNotificationTemplateListByBirthdayTemplateID(string BirthdayTemplateID)
        {
            List<clsBirthdayNotificationTemplate> NewsList = new List<clsBirthdayNotificationTemplate>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_GET_BirthdayNotificationTemplateByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BirthdayTemplateID", BirthdayTemplateID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    List<string> data = new List<string>();
                    data.Add(dt.Rows[0]["Subject"].ToString());
                    data.Add(dt.Rows[0]["Content"].ToString());

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = data;
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

        public clsResponse InsertUpdate(clsBirthdayNotificationTemplate data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_Ins_BirthdayNotificationTemplate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BirthdayTemplateID", data.BirthdayTemplateID ?? "");
                    cmd.Parameters.AddWithValue("Subject", data.Subject ?? "");
                    cmd.Parameters.AddWithValue("Content", data.Content ?? "");

                    cmd.Parameters.AddWithValue("ImageData", data.ImageData ?? "");

                    cmd.Parameters.AddWithValue("UserCreate", data.CreateUser ?? "");

                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    Response.ID = 1;
                    Response.Message = dt.Rows[0]["Message"].ToString();
                    Response.Contents = dt.Rows[0]["ID"].ToString();
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

        public clsResponse Delete(clsBirthdayNotificationTemplate data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_BirthdayTemplate_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BirthdayTemplateID", data.BirthdayTemplateID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    Response.ID = 1;
                    Response.Message = dt.Rows[0]["Message"].ToString();
                    Response.Contents = "";
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
    }
}