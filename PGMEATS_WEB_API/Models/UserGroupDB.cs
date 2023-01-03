using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PGMEATS_WEB_API.Models;

namespace PGMEATS_WEB_API.Models
{
    public class UserGroup
    {
        public string GroupID { get; set; }
        public string GroupDescription { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
    }

    public class UserGroupDB
    {
        public IEnumerable<UserGroup> UserGroupList(string GroupID)
        {
            try
            {
                List<UserGroup> Users = new List<UserGroup>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserGroup_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("GroupID", GroupID.ToString());
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        UserGroup User = new UserGroup();
                        User.GroupID = rd["GroupID"].ToString();
                        User.GroupDescription = rd["GroupDescription"].ToString();
                        if (!Convert.IsDBNull(rd["LastUser"]))
                        {
                            User.LastUser = rd["LastUser"].ToString();
                        }
                        if (!Convert.IsDBNull(rd["LastUpdate"]))
                        {
                            User.LastUpdate = rd["LastUpdate"].ToString();
                        }
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

        public int Insert(UserGroup User, string UserLogin)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserGroup_InsUpd";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("GroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("GroupDescription", User.GroupDescription);
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

        public int Delete(string GroupID)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserGroup_Del";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("GroupID", GroupID);
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