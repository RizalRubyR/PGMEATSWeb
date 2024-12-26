using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace PGMEATS_WEB.Models
{
    public class clsMyKY
    {
        public string MyKYID { get; set; }
        public string MyKYDate { get; set; }
        public string MyKYLocation { get; set; }
        public string MyKYLocationDesc { get; set; }
        public string MyKYSpecLocation { get; set; }
        public string MyKYStatus { get; set; }
        public string MyKYStatusDesc { get; set; }
        public string MyKYDesc { get; set; }
        public string MyKYReply { get; set; }
        public string Evidence { get; set; }
        public string EvidenceAfter { get; set; }
        public string FileNameEvidenceAfter { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
        public string Periode { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Section  { get; set; }
        public string Shift { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string KYID_Eats { get; set; }
        public string UserID { get; set; }
    }

    public class clsMyKYDB
    {
        public clsResponse ReplyMyKYList(clsMyKY data)
        {
            List<clsMyKY> Menus = new List<clsMyKY>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyMyKY_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("FromDate", data.FromDate);
                    cmd.Parameters.AddWithValue("ToDate", data.ToDate);
                    cmd.Parameters.AddWithValue("MyKYLocation", data.MyKYLocation);
                    cmd.Parameters.AddWithValue("MyKYStatus", data.MyKYStatus);
                    cmd.Parameters.AddWithValue("UserID", data.UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsMyKY Menu = new clsMyKY();
                        Menu.MyKYID = rd["MyKYID"].ToString();
                        Menu.MyKYDate = rd["MyKYDate"].ToString();
                        Menu.MyKYLocation = rd["MyKYLocation"].ToString();
                        Menu.MyKYLocationDesc = rd["MyKYLocationDesc"].ToString();
                        Menu.MyKYSpecLocation = rd["MyKYSpecLocation"].ToString();
                        Menu.MyKYDesc = rd["MyKYDesc"].ToString();
                        Menu.MyKYReply = rd["MyKYReply"].ToString();
                        Menu.MyKYStatusDesc = rd["MyKYStatusDesc"].ToString();
                        Menu.Evidence = rd["Evidence"].ToString();
                        Menu.EvidenceAfter = rd["EvidenceAfter"].ToString();
                        Menu.Section = rd["Section"].ToString();
                        Menu.Shift = rd["Shift"].ToString();
                        Menu.CreateDate = rd["CreateDate"].ToString();
                        Menu.CreateUser = rd["CreateUser"].ToString();
                        Menu.LastUpdate = rd["LastUpdate"].ToString();
                        Menu.LastUser = rd["LastUser"].ToString();
                        Menu.KYID_Eats = rd["KYID_Eats"].ToString();
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
        public List<clsMyKY> ReplyMyKYListExcel(clsMyKY data)
        {
            List<clsMyKY> Menus = new List<clsMyKY>();
        
           
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyMyKY_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("FromDate", data.FromDate);
                    cmd.Parameters.AddWithValue("ToDate", data.ToDate);
                    cmd.Parameters.AddWithValue("MyKYLocation", data.MyKYLocation);
                    cmd.Parameters.AddWithValue("MyKYStatus", data.MyKYStatus);
                    cmd.Parameters.AddWithValue("UserID", data.UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsMyKY Menu = new clsMyKY();
                        Menu.MyKYID = rd["MyKYID"].ToString();
                        Menu.MyKYDate = rd["MyKYDate"].ToString();
                        Menu.MyKYLocation = rd["MyKYLocation"].ToString();
                        Menu.MyKYLocationDesc = rd["MyKYLocationDesc"].ToString();
                        Menu.MyKYSpecLocation = rd["MyKYSpecLocation"].ToString();
                        Menu.MyKYDesc = rd["MyKYDesc"].ToString();
                        Menu.MyKYReply = rd["MyKYReply"].ToString();
                        Menu.MyKYStatusDesc = rd["MyKYStatusDesc"].ToString();
                        Menu.Evidence = rd["Evidence"].ToString();
                        Menu.EvidenceAfter = rd["EvidenceAfter"].ToString();
                        Menu.CreateDate = rd["CreateDate"].ToString();
                        Menu.Section = rd["Section"].ToString();
                        Menu.Shift = rd["Shift"].ToString();
                    Menu.CreateUser = rd["CreateUser"].ToString();
                        Menu.LastUpdate = rd["LastUpdate"].ToString();
                        Menu.LastUser = rd["LastUser"].ToString();
                        Menu.KYID_Eats = rd["KYID_Eats"].ToString();
                        Menus.Add(Menu);
                    }

                    return Menus;
                }
            
           
        }
        public clsResponse ReplyMyKYSel(String MyKYID)
        {
            clsMyKY Menu = new clsMyKY();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyMyKY_Sel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MyKYID", MyKYID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                       
                        Menu.MyKYID = rd["MyKYID"].ToString();
                        Menu.KYID_Eats = rd["KYID_Eats"].ToString();
                        Menu.MyKYLocationDesc = rd["MyKYLocationDesc"].ToString();
                        Menu.MyKYSpecLocation = rd["MyKYSpecLocation"].ToString();
                        Menu.EvidenceAfter = rd["EvidenceAfter"].ToString();
                        Menu.MyKYDesc = rd["MyKYDesc"].ToString();
                        Menu.MyKYReply = rd["MyKYReply"].ToString();
                        Menu.MyKYStatus = rd["Status"].ToString();
                        Menu.CreateDate = rd["CreateDate"].ToString();
                        Menu.CreateUser = rd["CreateUser"].ToString();
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
        public clsMyKY ReplyMyKYSelEvidenceAfter(String MyKYID)
        {
            clsMyKY Menu = new clsMyKY();
            
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyMyKYEvidenceAfter_Sel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MyKYID", MyKYID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {

                        Menu.MyKYID = rd["MyKYID"].ToString();
                        Menu.EvidenceAfter = rd["EvidenceAfter"].ToString();
                    }

                 
                }
            
            return Menu;
        }
        public clsResponse ReplyMyKYUpd(clsMyKY data)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyMyKY_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MyKYID", data.MyKYID);
                    cmd.Parameters.AddWithValue("MyKYReply", data.MyKYReply);
                    cmd.Parameters.AddWithValue("Status", data.MyKYStatus);
                    cmd.Parameters.AddWithValue("MyKYLoc", data.MyKYLocation);
                    cmd.Parameters.AddWithValue("CreateUser", data.CreateUser);
                    cmd.Parameters.AddWithValue("EvidenceAfter", data.EvidenceAfter);
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

        public clsResponse ReplyMyKYDel(string MyKYID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReplyMyKY_Del", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MyKYID", MyKYID);
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