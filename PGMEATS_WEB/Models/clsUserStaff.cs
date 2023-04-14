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
    public class clsUserStaff
    {
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Shift { get; set; }
        public string ActiveStatus { get; set; }
        public string CardNo { get; set; }
        [DisplayName("Approval Status")]
        public string ApprovalStatus { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ICNo { get; set; }
    }


    public class clsUserStaffDB
    {
        public IEnumerable<clsUserStaff> UserList()
        {
            try
            {
                Encryption encrypt = new Encryption();

                List<clsUserStaff> Users = new List<clsUserStaff>();

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserStaff";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 1);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserStaff User = new clsUserStaff();
                        User.StaffID = rd["StaffID"].ToString();
                        User.StaffName = rd["StaffName"].ToString().Trim();
                        User.Department = rd["Department"].ToString();
                        User.Section = rd["Section"].ToString();
                        User.Shift = rd["Shift"].ToString().Trim();
                        User.ActiveStatus = rd["ActiveStatus"].ToString();
                        User.CardNo = rd["CardNo"].ToString();
                        User.ApprovalStatus = rd["ApprovalStatus"].ToString();
                        User.ICNo = rd["ICNo"].ToString();

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

        public IEnumerable<clsUserStaff> UserListByUserID(string StaffID)
        {
            try
            {
                Encryption encrypt = new Encryption();

                List<clsUserStaff> Users = new List<clsUserStaff>();

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserStaff";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 2);
                    cmd.Parameters.AddWithValue("StaffID", StaffID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsUserStaff User = new clsUserStaff();
                        User.StaffID = rd["StaffID"].ToString();
                        User.StaffName = rd["StaffName"].ToString().Trim();
                        User.Department = rd["Department"].ToString();
                        User.Section = rd["Section"].ToString();
                        User.Shift = rd["Shift"].ToString().Trim();
                        User.ActiveStatus = rd["ActiveStatus"].ToString();
                        User.CardNo = rd["CardNo"].ToString();
                        User.ApprovalStatus = rd["ApprovalStatus"].ToString();
                        User.Email = rd["Email"].ToString();
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

        public int Disabled(string StaffID)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserStaff";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 3);
                    cmd.Parameters.AddWithValue("StaffID", StaffID);
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

        public int Enabled(string StaffID)
        {
            try
            {
                int i = 0;

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserStaff";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 4);
                    cmd.Parameters.AddWithValue("StaffID", StaffID);
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

        public int Update(clsUserStaff User, string UserLogin)
        {
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserStaff";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 5);
                    cmd.Parameters.AddWithValue("StaffID", User.StaffID);
                    cmd.Parameters.AddWithValue("Email", User.Email);
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

        public int ResetPassword(clsUserStaff User, string UserLogin)
        {
            EncryptionUserStaff encrypt = new EncryptionUserStaff();
            try
            {
                int i = 0;
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_UserStaff";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Func", 6);
                    cmd.Parameters.AddWithValue("StaffID", User.StaffID);
                    cmd.Parameters.AddWithValue("Password", encrypt.Encrypt(User.ICNo, User.StaffID.ToUpper()));
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