using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class clsConPathFolder
    {
        public string AppID { get; set; }
        public string Menu { get; set; }
        public string Path { get; set; }    
    }

    public class clsConPathFolderDB
    {
        public string PathFolder(string AppID, string Menu)
        {
            string Path = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_ConPathFolder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AppID",AppID);
                    cmd.Parameters.AddWithValue("Menu",Menu);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                       Path = rd["Path"].ToString();
                    }
                    return Path;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}