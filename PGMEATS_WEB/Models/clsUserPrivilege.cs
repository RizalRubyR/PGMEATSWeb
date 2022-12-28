using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ADLESKAP.Models;


namespace ADLESKAP.Models
{
    public class ClsUserPrivilegeViewModel
    {
        public List<clsUserPrivilege> Privileges { get; set; }

        public ClsUserPrivilegeViewModel()
        {
            this.Privileges = new List<clsUserPrivilege>();
        }
    }

    public class clsUserPrivilege
    {
        public string UserID { get; set; }
        public string GroupID { get; set; }
        public string MenuID { get; set; }
        public string MenuDesc { get; set; }
        public bool AllowAccess { get; set; }
    }

    public class clsUserPrivilegeDB
    {
        public IEnumerable<clsUserPrivilege> UserPrivilegeList(string UserID)
        {
            try
            {
                List<clsUserPrivilege> Users = new List<clsUserPrivilege>();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserPrivilege";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 1);
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserPrivilege User = new clsUserPrivilege();
                        User.UserID = rd["UserID"].ToString();
                        User.GroupID = rd["GroupID"].ToString();
                        User.MenuID = rd["MenuID"].ToString().Trim();
                        User.MenuDesc = rd["MenuDesc"].ToString();
                        User.AllowAccess = rd["AllowAccess"].ToString().Trim() == "1" ? true : false ;

                        Users.Add(User);
                    }
                    return Users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Update(clsUserPrivilege User, string UserLogin)
        {
            try
            {
                Encryption encrypt = new Encryption();
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserPrivilege";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 2);
                    cmd.Parameters.AddWithValue("UserID", User.UserID);
                    cmd.Parameters.AddWithValue("MenuID", User.MenuID);
                    cmd.Parameters.AddWithValue("AllowAccess", User.AllowAccess);
                    cmd.Parameters.AddWithValue("UserLogin", UserLogin);
                    con.Open();

                    i = cmd.ExecuteNonQuery();
                }
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}