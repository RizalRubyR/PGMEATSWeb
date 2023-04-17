using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace PGMEATS_WEB.Models
{
    public class clsUserPrivilege
    {
        public string GroupID { get; set; }
        public string MenuID { get; set; }
        public string MenuDesc { get; set; }
        public string AllowAccess { get; set; }
        public string AllowUpdate { get; set; }
        public string UserID { get; set; }
        public string UserLogin { get; set; }
        public Boolean Access { get; set; }
        public Boolean Update { get; set; }
        public string AdminStatus{ get; set; }
    }
    public class clsUserPrivilegeDB
    {
        public clsResponse UserPrivilegeSel(string Func, string UserID)
        {
            List<clsUserPrivilege> Menus = new List<clsUserPrivilege>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_UserPrivilege", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", Func);
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var AllowAccess = false;
                        var AllowUpdate = false;
                        if (rd["AllowAccess"].ToString() == "1"){ AllowAccess = true; };
                        if (rd["AllowUpdate"].ToString() == "1") { AllowUpdate = true; };

                        clsUserPrivilege Menu = new clsUserPrivilege();
                        Menu.MenuID = rd["MenuID"].ToString();
                        Menu.MenuDesc = rd["MenuDesc"].ToString();
                        Menu.Access = AllowAccess;
                        Menu.Update = AllowUpdate;
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

        public clsResponse UserPrivilegeUpd(string Func, clsUserPrivilege data, string UserLogin)
        {
            List<clsUserPrivilege> Menus = new List<clsUserPrivilege>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_UserPrivilege", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", Func);
                    cmd.Parameters.AddWithValue("UserID", data.UserID);
                    cmd.Parameters.AddWithValue("MenuID", data.MenuID);
                    cmd.Parameters.AddWithValue("AllowAccess", data.AllowAccess);
                    cmd.Parameters.AddWithValue("AllowUpdate", data.AllowUpdate);
                    cmd.Parameters.AddWithValue("UserLogin", UserLogin);
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

        public clsUserPrivilege UserPrivilegeCheck(string Func, clsUserPrivilege data)
        {
            clsUserPrivilege privilege = new clsUserPrivilege();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_UserPrivilege", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", Func);
                    cmd.Parameters.AddWithValue("UserID", data.UserID);
                    cmd.Parameters.AddWithValue("MenuID", data.MenuID);
                    cmd.Parameters.AddWithValue("AdminStatus", data.AdminStatus);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        privilege.AllowAccess = rd["AllowAccess"].ToString();
                        privilege.AllowUpdate = rd["AllowUpdate"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return privilege;
        }

    }
}