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
    public class AccessPrivilegeDB
    {
        public string CheckUserPrevilege(string pUserID, string pMenuID, string pAllowType)
        {
            try
            {
                string check = "";

                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {    
                    string sql = "usp_AccessPrivilege_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", pUserID);
                    cmd.Parameters.AddWithValue("MenuID", pMenuID);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    //Allow Type value 1 : (Access), 2  : (Create/Update data)
                    if (pAllowType == "1")
                    {
                        check = dt.Rows[0]["AllowAccess"].ToString();
                    }
                    else if (pAllowType == "2")
                    {
                        check = dt.Rows[0]["AllowUpdate"].ToString();
                    }

                    return check;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}