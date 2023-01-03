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
    public class User
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string GroupID { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
    }

    public class UserType
    {
        public string UserTypeCode { get; set; }
    }

    public class UserGroups
    {
        public string GroupID { get; set; }
        public string GroupDescription { get; set; }
    }

    public class UserResponse
    {
        public int RespID { get; set; }
        public string RespDesc { get; set; }
    }

    public class ChangePassword
    {
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public class UserDB
    {
        public int UserCheck(string UserID, string Password)
        {
            try
            {
                Encryption encrypt = new Encryption();

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_CheckUser";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID.ToString());
                    cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(Password, UserID));
                    con.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<User> UserList(string UserID)
        {
            try
            {
                Encryption encrypt = new Encryption();

                List<User> Users = new List<User>();

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID.ToString());
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        User User = new User();
                        User.UserID = rd["UserID"].ToString();
                        User.FullName = rd["FullName"].ToString();
                        User.Password = rd["Password"].ToString();
                        User.Password = encrypt.Decrypt(User.Password, User.UserID);
                        User.UserType = rd["UserType"].ToString();
                        User.GroupID = rd["GroupID"].ToString();
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

        public IEnumerable<UserType> UserTypeList()
        {
            try
            {
                List<UserType> Users = new List<UserType>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_GetUserType";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        UserType User = new UserType();
                        User.UserTypeCode = rd["UserTypeCode"].ToString();
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

        public IEnumerable<UserGroups> UserGroupList()
        {
            try
            {
                List<UserGroups> Users = new List<UserGroups>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_GetUserGroup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        UserGroups User = new UserGroups();
                        User.GroupID = rd["GroupID"].ToString();
                        User.GroupDescription = rd["GroupDescription"].ToString();
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

        public int Insert(User User, string UserLogin)
        {
            try
            {
                Encryption encrypt = new Encryption();

                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_InsUpd";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", User.UserID);
                    cmd.Parameters.AddWithValue("FullName", User.FullName);
                    cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(User.Password, User.UserID));
                    cmd.Parameters.AddWithValue("GroupID", User.GroupID);
                    cmd.Parameters.AddWithValue("UserType", User.UserType);
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

        public int Delete(string UserID)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_Del";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", UserID);
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

        public int ChangePassword(ChangePassword password)
        {
            try
            {
                Encryption encrypt = new Encryption();

                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using(SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_UserSetup_ChangePassword";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", password.UserID);
                    cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(password.Password, password.UserID));
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