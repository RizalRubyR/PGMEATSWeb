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
    public class clsPasswordHistory
    {
        // Inherits clsCon

        private string _UserID;
        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        private int _SeqNo;
        public int SeqNo
        {
            get
            {
                return _SeqNo;
            }
            set
            {
                _SeqNo = value;
            }
        }

        private DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get
            {
                return _UpdateDate;
            }
            set
            {
                _UpdateDate = value;
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
    }

    public class clsPasswordHistoryDB
    {
        public static clsPasswordHistory GetLastData(clsPasswordHistory pObj)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "select top 1 * from M_JSOXPasswordHistory where UserID = @UserID order by UpdateDate desc";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@UserID", pObj.UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsPasswordHistory History = new clsPasswordHistory();
                        History.UserID = rd["UserID"].ToString();
                        History.SeqNo = int.Parse(rd["SeqNo"].ToString());
                        History.UpdateDate = DateTime.Parse(rd["UpdateDate"].ToString());
                        History.Password = rd["Password"].ToString();
                        return History;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetServerDate()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string q = "Select GetDate()";
                SqlCommand cmd = new SqlCommand(q, con);
                DateTime ServerDate = DateTime.Parse(cmd.ExecuteScalar().ToString());
                return ServerDate;
            }
        }

        public IEnumerable<clsPasswordHistory> GetList(string UserID, string Retain)
        {
            try
            {
                List<clsPasswordHistory> Users = new List<clsPasswordHistory>();

                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "select top " + Retain + " * from M_JSOXPasswordHistory where UserID = @UserID order by UpdateDate desc";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("UserID", UserID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        clsPasswordHistory User = new clsPasswordHistory();
                        User.UserID = rd["UserID"].ToString();
                        User.SeqNo = int.Parse(rd["SeqNo"].ToString().Trim());
                        User.UpdateDate = DateTime.Parse(rd["UpdateDate"].ToString().Trim());
                        User.Password = rd["Password"].ToString();

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

    }
}