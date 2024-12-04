using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PGMEATS_WEB.Models;

namespace PGMEATS_WEB.Models
{
    public class Response
    {
        public string Message { get; set; }
        public string ID { get; set; }
        public Object Content { get; set; }
    }
    public class ComboFilter
    {
        public string Code { get; set; }
        public string CodeDesc { get; set; }


        public Response FillCombo(String Action, String param1)
        {
            Response resp = new Response();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_Web_FillCombo";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Action", Action);
                    cmd.Parameters.AddWithValue("Param", param1);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    List<ComboFilter> cb = new List<ComboFilter>();
                    while (rd.Read())
                    {
                        ComboFilter cbDet = new ComboFilter();
                        cbDet.Code = rd["Code"].ToString();
                        cbDet.CodeDesc = rd["CodeDesc"].ToString();
                        cb.Add(cbDet);
                    }
                    resp.Message = "success";
                    resp.ID = "0";
                    resp.Content = cb;
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.ID = "1";
                resp.Content = "";
                return resp;
            }
        }

        public Response FillComboUser()
        {
            Response resp = new Response();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql =
                        "select '' UserID, '-' UserName, 1 seq union " +
                        "select upper(UserID) UserID, upper(UserID) UserName, 2 seq from M_UserSetup where (UserType = 0 or UserType = 2) " +
                        "order by Seq, UserName ";
                    SqlCommand cmd = new SqlCommand(sql, con);                    
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    List<ComboFilter> cb = new List<ComboFilter>();
                    while (rd.Read())
                    {
                        ComboFilter cbDet = new ComboFilter();
                        cbDet.Code = rd["UserID"].ToString();
                        cbDet.CodeDesc = rd["UserName"].ToString();
                        cb.Add(cbDet);
                    }
                    resp.Message = "success";
                    resp.ID = "0";
                    resp.Content = cb;
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.ID = "1";
                resp.Content = "";
                return resp;
            }
        }


        public Response FillComboResponse()
        {
            Response resp = new Response();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "select ResponseID, Description from M_ResponseTemplate ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    List<ComboFilter> cb = new List<ComboFilter>();
                    while (rd.Read())
                    {
                        ComboFilter cbDet = new ComboFilter();
                        cbDet.Code = rd["UserID"].ToString();
                        cbDet.CodeDesc = rd["UserName"].ToString();
                        cb.Add(cbDet);
                    }
                    resp.Message = "success";
                    resp.ID = "0";
                    resp.Content = cb;
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.ID = "1";
                resp.Content = "";
                return resp;
            }
        }
    }
}