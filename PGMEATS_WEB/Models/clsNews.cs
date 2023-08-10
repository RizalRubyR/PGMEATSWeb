using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsNews
    {
        public string User { get; set; }
        public string NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescCode { get; set; }
        public string NewsDescText { get; set; }
        public string NewsDescImg { get; set; }
        public string Attachment { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string TargetPart { get; set; }
        public string TargetDesignation { get; set; }
        public string Status { get; set; }
    }

    public class clsNewsDB
    {
        public clsResponse NewsList(string User, string datefrom, string dateTo, string groupdepartment, string designation, string status)
        {
            List<clsNews> NewsList = new List<clsNews>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("User", User);
                    cmd.Parameters.AddWithValue("DateFrom", datefrom);
                    cmd.Parameters.AddWithValue("DateTo", dateTo);
                    cmd.Parameters.AddWithValue("GroupDept", groupdepartment);
                    cmd.Parameters.AddWithValue("SALPlan", designation);
                    cmd.Parameters.AddWithValue("Status", status);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    NewsList = dt.AsEnumerable().Select(x =>
                    new clsNews
                    {
                        NewsID = x.Field<Int32>("NewsID").ToString(),
                        NewsTitle = x.Field<string>("NewsTitle"),
                        NewsDescCode = x.Field<string>("NewsDescCode"),
                        NewsDescText = x.Field<string>("NewsDescText"),
                        Attachment = x.Field<string>("Attachment"),
                        DateFrom = x.Field<string>("DateFrom"),
                        DateTo = x.Field<string>("DateTo"),
                        TargetPart = x.Field<string>("TargetPart"),
                        TargetDesignation = x.Field<string>("TargetDesignation"),
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = NewsList;
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

        public clsResponse GetDataDetail(string NewsID)
        {
            List<clsNews> NewsList = new List<clsNews>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NewsID", NewsID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    List<string> data = new List<string>();
                    data.Add(dt.Rows[0]["NewsTitle"].ToString());
                    data.Add(dt.Rows[0]["NewsDescCode"].ToString());
                    data.Add(dt.Rows[0]["DateFrom"].ToString());
                    data.Add(dt.Rows[0]["DateTo"].ToString());
                    data.Add(dt.Rows[0]["TargetPart"].ToString());
                    data.Add(dt.Rows[0]["TargetDesignation"].ToString());

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

        public clsResponse GetDataDetailPopUp(string NewsID)
        {
            string data = "";
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NewsID", NewsID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    data = dt.Rows[0]["NewsDescCode"].ToString();

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

        public clsResponse FillCombo(string type)
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_FillCombo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Type", type);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("Code"),
                        Description = x.Field<string>("Description")                        
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
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

        public clsResponse InsertUpdate(clsNews data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_InsUpd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NewsID", data.NewsID ?? "0");
                    cmd.Parameters.AddWithValue("NewsTitle", data.NewsTitle ?? "");
                    cmd.Parameters.AddWithValue("NewsDescCode", data.NewsDescCode ?? "");
                    cmd.Parameters.AddWithValue("NewsDescText", data.NewsDescText ?? "");
                    cmd.Parameters.AddWithValue("NewsDescImg", data.NewsDescImg ?? "");
                    cmd.Parameters.AddWithValue("DateFrom", data.DateFrom ?? "");
                    cmd.Parameters.AddWithValue("DateTo", data.DateTo ?? "");
                    cmd.Parameters.AddWithValue("GroupDepartment", data.TargetPart ?? "");
                    cmd.Parameters.AddWithValue("SALPLan", data.TargetDesignation ?? "");
                    cmd.Parameters.AddWithValue("User", data.User ?? "");
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    Response.ID = 1;
                    Response.Message = dt.Rows[0]["Message"].ToString();
                    Response.Contents = dt.Rows[0]["ID"].ToString() + "|" + dt.Rows[0]["OldFile"].ToString();
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

        public clsResponse Delete(clsNews data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NewsID", data.NewsID);
                    cmd.Parameters.AddWithValue("Type", 0);
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

                    data.Attachment = dt.Rows[0]["OldFile"].ToString();
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

        public clsResponse DeleteAttachment(clsNews data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NewsID", data.NewsID);
                    cmd.Parameters.AddWithValue("Type", 1);
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

                    data.Attachment = dt.Rows[0]["OldFile"].ToString();
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

        public clsResponse UpdateFile(clsNews data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_News_UpdateAttachment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NewsID", data.NewsID ?? "0");
                    cmd.Parameters.AddWithValue("FileName", data.Attachment ?? "");
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