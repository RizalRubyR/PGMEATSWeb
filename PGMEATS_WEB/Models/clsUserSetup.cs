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
    public class clsUserSetup
    {
        [DisplayName("User ID")]
        [Required]
        public string UserID { get; set; }


        [DisplayName("Full Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Admin Status")]
        public string AdminStatus { get; set; }

        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
        public string Department { get; set; }
        public string UserType { get; set; }
    }


    public class clsUserSetupDB
    {
        public IEnumerable<clsUserSetup> UserList()
        {
            try
            {
                Encryption encrypt = new Encryption();

                List<clsUserSetup> Users = new List<clsUserSetup>();

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 1);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserSetup User = new clsUserSetup();
                        User.UserID = rd["UserID"].ToString();
                        User.UserName = rd["UserName"].ToString().Trim();
                        User.UserType = rd["UserType"].ToString();
                        User.Department = rd["Department"].ToString();
                        User.AdminStatus = rd["AdminStatus"].ToString();
                        User.LastUser = rd["LastUser"].ToString().Trim();
                        User.LastUpdate = rd["LastUpdate"].ToString();

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

        public Response UserListByUserID(string UserID)
        {
            Response resp = new Response();
            try
            {
                Encryption encrypt = new Encryption();
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 2);
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    clsUserSetup User = new clsUserSetup();
                    while (rd.Read())
                    {
                        
                        User.UserID = rd["UserID"].ToString();
                        User.UserName = rd["UserName"].ToString().Trim();
                        User.Password = encrypt.DecryptData(rd["Password"].ToString().Trim());
                        User.UserType = rd["UserType"].ToString();
                        User.Department = rd["Department"].ToString();
                        User.AdminStatus = rd["AdminStatus"].ToString();
                        User.LastUser = rd["LastUser"].ToString().Trim();
                        User.LastUpdate = rd["LastUpdate"].ToString();
                    }
                    resp.Message = "Success";
                    resp.ID = "0";
                    resp.Content = User;
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

        public int Insert(clsUserSetup User, string UserLogin)
        {
            try
            {
                Encryption encrypt = new Encryption();
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 3);
                    cmd.Parameters.AddWithValue("UserID", User.UserID);
                    cmd.Parameters.AddWithValue("UserName", User.UserName);
                    cmd.Parameters.AddWithValue("Password", encrypt.EncryptData(User.Password.Trim()));
                    cmd.Parameters.AddWithValue("AdminStatus", User.AdminStatus);
                    cmd.Parameters.AddWithValue("UserType", User.UserType);
                    cmd.Parameters.AddWithValue("Department", User.Department);
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

        public int Update(clsUserSetup User, string UserLogin)
        {
            try
            {
                Encryption encrypt = new Encryption();
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 4);
                    cmd.Parameters.AddWithValue("UserID", User.UserID);
                    cmd.Parameters.AddWithValue("UserName", User.UserName);
                    cmd.Parameters.AddWithValue("AdminStatus", User.AdminStatus);
                    cmd.Parameters.AddWithValue("UserType", User.UserType);
                    cmd.Parameters.AddWithValue("Department", User.Department);
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

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 5);
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

        public int InsertJSOXHistory(clsUserSetup User, string UserLogin)
        {
            try
            {
                Encryption encrypt = new Encryption();
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserSetup";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 6);
                    cmd.Parameters.AddWithValue("UserID", User.UserID);
                    cmd.Parameters.AddWithValue("Password", encrypt.EncryptData(User.Password.Trim()));
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