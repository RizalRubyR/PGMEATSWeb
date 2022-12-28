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
    public class clsJSOXSetup
    {
        // Inherits clsCon

        private string _RuleID;
        public string RuleID
        {
            get
            {
                return _RuleID;
            }
            set
            {
                _RuleID = value;
            }
        }

        private bool _Enable;
        public bool Enable
        {
            get
            {
                return _Enable;
            }
            set
            {
                _Enable = value;
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        private int _ParamValue;
        public int ParamValue
        {
            get
            {
                return _ParamValue;
            }
            set
            {
                _ParamValue = value;
            }
        }

        private string _UpdateUser;
        public string UpdateUser
        {
            get
            {
                return _UpdateUser;
            }
            set
            {
                _UpdateUser = value;
            }
        }
    }

    public class clsJSOXSetupDB
    {
        public static clsJSOXSetup GetRule(clsJSOXSetup pObj)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "select * from M_JSOXSetup where RuleID = @RuleID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@RuleID", pObj.RuleID);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    clsJSOXSetup User = new clsJSOXSetup();
                    while (rd.Read())
                    {
                        User.RuleID = rd["RuleID"].ToString();
                        User.Enable = rd["Enable"].ToString().Trim() == "1" ? true: false;
                        User.Description = rd["Description"].ToString().Trim();
                        User.ParamValue = int.Parse(rd["ParamValue"].ToString());
                    }
                    return User;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}