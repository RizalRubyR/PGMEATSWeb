﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsClinicInformationListDB
    {
        public clsResponse ClinicInformationList()
        {
            List<clsClinicInformationList> GroupClinic = new List<clsClinicInformationList>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetClinic", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsClinicInformationList clsClinic = new clsClinicInformationList();
                        clsClinic.ClinicName = rd["ClinicName"].ToString();
                        clsClinic.State = rd["State"].ToString();
                        clsClinic.Address = rd["Address"].ToString();
                        clsClinic.Phone_No = rd["Phone_No"].ToString();
                        clsClinic.Remark = rd["Remark"].ToString();
                        clsClinic.URL = rd["URL"].ToString();
                        clsClinic.URLDisplay = rd["URLDisplay"].ToString();
                        clsClinic.City = rd["City"].ToString();
                        clsClinic.PostalCode = rd["PostalCode"].ToString();
                        clsClinic.OperationHour = rd["OperationHours"].ToString();
                        clsClinic.Operation24Hours = Convert.ToChar(rd["Operation24Hours"]);


                        GroupClinic.Add(clsClinic);
                    }

                    Response.Message = "Success";
                    Response.Contents = GroupClinic;
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

        public IEnumerable<clsClinicInformationList> ClinicInformationListx
        {
            get
            {
                List<clsClinicInformationList> GroupClinic = new List<clsClinicInformationList>();
                clsResponse Response = new clsResponse();
                try
                {
                    string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("sp_GetClinic", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        SqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            clsClinicInformationList clsClinic = new clsClinicInformationList();
                            clsClinic.ClinicID = Convert.ToInt16(rd["ClinicID"]);
                            clsClinic.ClinicName = rd["ClinicName"].ToString();
                            clsClinic.State = rd["State"].ToString();
                            clsClinic.Address = rd["Address"].ToString();
                            clsClinic.Phone_No = rd["Phone_No"].ToString();
                            clsClinic.Remark = rd["Remark"].ToString();
                            clsClinic.URL = rd["URL"].ToString();
                            clsClinic.URLDisplay = rd["URLDisplay"].ToString();
                            clsClinic.City = rd["City"].ToString();
                            clsClinic.PostalCode = rd["PostalCode"].ToString();
                            clsClinic.OperationHour = rd["OperationHours"].ToString();
                            clsClinic.Operation24Hours = Convert.ToChar(rd["Operation24Hours"]);



                            GroupClinic.Add(clsClinic);
                        }

                        Response.Message = "Success";
                        Response.Contents = GroupClinic;
                    }
                }
                catch (Exception ex)
                {
                    Response.ID = 0;
                    Response.Message = ex.Message;
                    Response.Contents = "";

                }
                return GroupClinic;
            }
        }

        public clsResponse ClinicInformationValidateBeforeInsert(string Region,string ClinicName)
        {
            bool bIsClinicExist = false;
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetClinicByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Region", Region ?? "");
                    cmd.Parameters.AddWithValue("ClinicName", ClinicName ?? "");


                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        bIsClinicExist = true;
                        Response.Message = "Data Already Exist!";
                    }

                    Response.Contents = bIsClinicExist;
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

        public clsResponse ClinicInformationIns(clsClinicInformationList dataFrom)
        {
            clsResponse Response = new clsResponse();
            clsClinicInformationList data = new clsClinicInformationList();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ClinicInformation_Ins", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClinicName", dataFrom.ClinicName ?? "");
                    cmd.Parameters.AddWithValue("State", dataFrom.State ?? "");
                    cmd.Parameters.AddWithValue("Address", dataFrom.Address ?? "");
                    cmd.Parameters.AddWithValue("PhoneNo", dataFrom.Phone_No ?? "");
                    cmd.Parameters.AddWithValue("Remark", dataFrom.Remark ?? "");
                    cmd.Parameters.AddWithValue("URL", dataFrom.URL ?? "");
                    cmd.Parameters.AddWithValue("URLDisplay", dataFrom.URLDisplay ?? "");
                    cmd.Parameters.AddWithValue("City", dataFrom.City ?? "");
                    cmd.Parameters.AddWithValue("PostalCode", dataFrom.PostalCode?? "");
                    cmd.Parameters.AddWithValue("Operation24Hours", dataFrom.Operation24Hours);
                    cmd.Parameters.AddWithValue("OperationHours", dataFrom.OperationHour ?? "");

                    cmd.Parameters.AddWithValue("CreateUser", dataFrom.CreateUser ?? "");
                    cmd.Parameters.AddWithValue("CreateDate", DateTime.Now);


                    con.Open();
                    cmd.ExecuteNonQuery();

                    Response.Message = "Successfully Saved data!";

                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }

            return Response;
        }

        public clsResponse ClinicInformationUpd(clsClinicInformationList dataFrom)
        {
            clsResponse Response = new clsResponse();
            clsClinicInformationList data = new clsClinicInformationList();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ClinicInformation_Upd", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClinicID", dataFrom.ClinicID);
                    cmd.Parameters.AddWithValue("ClinicName", dataFrom.ClinicName ?? "");
                    cmd.Parameters.AddWithValue("State", dataFrom.State ?? "");
                    cmd.Parameters.AddWithValue("Address", dataFrom.Address ?? "");
                    cmd.Parameters.AddWithValue("PhoneNo", dataFrom.Phone_No ?? "");
                    cmd.Parameters.AddWithValue("Remark", dataFrom.Remark ?? "");
                    cmd.Parameters.AddWithValue("URL", dataFrom.URL ?? "");
                    cmd.Parameters.AddWithValue("URLDisplay", dataFrom.URLDisplay ?? "");
                    cmd.Parameters.AddWithValue("City", dataFrom.City?? "");
                    cmd.Parameters.AddWithValue("PostalCode", dataFrom.PostalCode?? "");
                    cmd.Parameters.AddWithValue("Operation24Hours", Convert.ToChar(dataFrom.Operation24Hours));
                    cmd.Parameters.AddWithValue("OperationHours", dataFrom.OperationHour ?? "");
                    cmd.Parameters.AddWithValue("UpdateUser", dataFrom.UpdateUser ?? "");
                    cmd.Parameters.AddWithValue("UpdateDate", DateTime.Now);


                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Response.Contents = rd["ClinicName"].ToString();
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

        public clsResponse ClinicInformationDel(string ClinicID)
        {
            clsResponse Response = new clsResponse();
            clsClinicInformationList data = new clsClinicInformationList();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ClinicInformation_Del", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ClinicID", ClinicID?? "");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();

                    Response.Message = "Delete Data Successfully !";

                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }

            return Response;
        }
        public clsResponse GetListState()
        {
            List<clsClinicInformationList> GroupClinic = new List<clsClinicInformationList>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetListState", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    GroupClinic = dt.AsEnumerable().Select(x =>
                    new clsClinicInformationList
                    {
                        State = x.Field<string>("StateID")
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = GroupClinic;
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