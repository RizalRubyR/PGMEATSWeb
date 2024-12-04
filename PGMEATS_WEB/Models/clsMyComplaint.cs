﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsMyComplaint
    {
        public string ComplaintID { get; set; }
        public string IssueTypeID { get; set; }
        public string IssueTypeDesc { get; set; }
        public string ComplaintDesc { get; set; }
        public string ComplaintReply { get; set; }
        public string ComplaintStatus { get; set; }
        public string ReplyStatus { get; set; }
        public string Evidence { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Department { get; set; }
        public string GroupDepartment { get; set; }
        public string CatererID { get; set; }
        public string ActionType { get; set; }
        public string UserID { get; set; }
        public string ResponseID { get; set; }
    }

    public class clsMyComplaintDB
    {
        public clsResponse ReplyComplaintList(clsMyComplaint data)
        {
            List<clsMyComplaint> Menus = new List<clsMyComplaint>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyComplaint_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("FromDate", data.FromDate);
                    cmd.Parameters.AddWithValue("ToDate", data.ToDate);
                    cmd.Parameters.AddWithValue("Department", data.Department);
                    cmd.Parameters.AddWithValue("IssueTypeID", data.IssueTypeID);
                    cmd.Parameters.AddWithValue("ComplaintStatus", data.ComplaintStatus);
                    cmd.Parameters.AddWithValue("UserID", data.UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsMyComplaint Menu = new clsMyComplaint();
                        Menu.ComplaintID = rd["ComplaintID"].ToString();
                        Menu.ComplaintDesc = rd["ComplaintDesc"].ToString().Replace(System.Environment.NewLine," ");
                        Menu.ComplaintReply = rd["ComplaintReply"].ToString();
                        Menu.ComplaintStatus = rd["ComplaintStatus"].ToString();
                        Menu.ReplyStatus = rd["ReplyStatus"].ToString();
                        Menu.IssueTypeDesc = rd["IssueTypeDesc"].ToString();
                        Menu.Evidence = rd["Evidence"].ToString();
                        Menu.CreatedUser = rd["CreatedUser"].ToString();
                        Menu.CreatedDate = rd["CreatedDate"].ToString();
                        Menu.LastUser = rd["LastUser"].ToString();
                        Menu.LastUpdate = rd["LastUpdate"].ToString();
                        Menu.Department = rd["Department"].ToString();
                        Menu.CatererID = rd["CatererID"].ToString();
                        Menus.Add(Menu);
                    }

                    Response.Message = "Success";
                    Response.Contents = Menus;
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse ReplyComplaintSel(String ComplaintID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                clsMyComplaint Menu = new clsMyComplaint();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyComplaint_Sel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ComplaintID", ComplaintID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Menu.ComplaintID = rd["ComplaintID"].ToString();
                        Menu.ComplaintDesc = rd["ComplaintDesc"].ToString().Replace(System.Environment.NewLine, " ");
                        Menu.ComplaintReply = rd["ComplaintReply"].ToString();
                        Menu.IssueTypeDesc = rd["IssueTypeDesc"].ToString();
                        Menu.CreatedUser = rd["CreatedUser"].ToString();
                        Menu.CreatedDate = rd["CreatedDate"].ToString();
                        Menu.Department = rd["Department"].ToString();
                        Menu.CatererID = rd["CatererID"].ToString();
                    }

                    Response.Message = "Success";
                    Response.Contents = Menu;
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse ReplyComplaintUpd(clsMyComplaint data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyComplaint_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("ActionType", data.ActionType);
                    cmd.Parameters.AddWithValue("ComplaintID", data.ComplaintID);
                    cmd.Parameters.AddWithValue("ComplaintReply", data.ComplaintReply);
                    cmd.Parameters.AddWithValue("CatererID", data.CatererID);
                    cmd.Parameters.AddWithValue("CreatedUser", data.CreatedUser);
                    cmd.Parameters.AddWithValue("ResponseID", data.ResponseID);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    Response.Message = "Successfully updated data!";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }

        public clsResponse ReplyComplaintDel(string ComplaintID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyComplaint_Del", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ComplaintID",ComplaintID);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    Response.Message = "Successfully deleted data!";
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }

        public clsResponse SettingCaterer(clsMyComplaint data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyComplaint_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", data.ActionType);
                    cmd.Parameters.AddWithValue("ComplaintID", data.ComplaintID);
                    cmd.Parameters.AddWithValue("CatererID", data.CatererID);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    Response.Message = "Successfully updated data!";
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

    
