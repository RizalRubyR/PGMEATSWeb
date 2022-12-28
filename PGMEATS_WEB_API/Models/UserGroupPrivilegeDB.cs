using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ADLESKAP_API.Models;

namespace ADLESKAP_API.Models
{
    public class UserGroupPrivilege
    {
        public string GroupID { get; set; }
        public string MenuID { get; set; }
        public string MenuGroup { get; set; }
        public string MenuDescription { get; set; }
        public bool AllowAccess { get; set; }
        public bool AllowUpdate { get; set; }

    }

    public class UserGroupPrivilegeDB
    {
        public IEnumerable<UserGroupPrivilege> UserGroupPrivilegeList(string GroupID)
        {
            try
            {
                List<UserGroupPrivilege> Menus = new List<UserGroupPrivilege>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserPrivilege_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("GroupID", GroupID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        UserGroupPrivilege Menu = new UserGroupPrivilege();
                        Menu.GroupID = GroupID;
                        Menu.MenuID = rd["MenuID"].ToString();
                        Menu.MenuGroup = rd["MenuGroup"].ToString();
                        Menu.MenuDescription = rd["MenuDescription"].ToString();
                        Menu.AllowAccess = Convert.ToBoolean(rd["AllowAccess"]);
                        Menu.AllowUpdate = Convert.ToBoolean(rd["AllowUpdate"]);
                        Menus.Add(Menu);
                    }
                    return Menus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdatePrivilege(UserGroupPrivilege privilege)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("usp_UserPrivilege_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("GroupID", privilege.GroupID);
                    cmd.Parameters.AddWithValue("MenuID", privilege.MenuID);
                    cmd.Parameters.AddWithValue("AllowAccess", Convert.ToInt16(privilege.AllowAccess));
                    cmd.Parameters.AddWithValue("AllowUpdate", Convert.ToInt16(privilege.AllowUpdate));
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