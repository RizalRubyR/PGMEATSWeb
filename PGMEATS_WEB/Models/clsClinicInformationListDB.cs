using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsClinicInformationListDB
    {
        public IEnumerable<clsClinicInformationList> Clinic
        {
            get
            {   
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                List<clsClinicInformationList> GroupClinic = new List<clsClinicInformationList>();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetClinic", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        clsClinicInformationList clsClinic = new clsClinicInformationList();
                        clsClinic.Region = sdr["Region"].ToString();
                        clsClinic.ClinicName = sdr["ClinicName"].ToString();
                        clsClinic.Description = sdr["Description"].ToString();
                        clsClinic.URL = sdr["URL"].ToString();
                        clsClinic.URLDisplay = sdr["URLDisplay"].ToString();
                        GroupClinic.Add(clsClinic);
                    }
                    sdr.Close();
                    cmd.Dispose();
                    return GroupClinic;
                }
            }
        }

        public clsClinicInformationList GetData(string pSource)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Select * From ms_Source Where Source = '" + pSource + "'", con);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    clsClinicInformationList src = new clsClinicInformationList();
                    src.Region = dt.Rows[0]["Region"].ToString();
                    src.ClinicName = dt.Rows[0]["ClinicName"].ToString();
                    src.Description = dt.Rows[0]["Description"].ToString();
                    src.URL = dt.Rows[0]["URL"].ToString();
                    src.URLDisplay = dt.Rows[0]["URLDisplay"].ToString();

                    return src;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Insert(clsClinicInformationList obj)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ClinicIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Region", obj.Region);
                cmd.Parameters.AddWithValue("ClinicName", obj.ClinicName);
                cmd.Parameters.AddWithValue("Description", obj.Description);
                cmd.Parameters.AddWithValue("URL", obj.URL);
                cmd.Parameters.AddWithValue("URLDisplay", obj.URLDisplay);
                cmd.Parameters.AddWithValue("CreateUser", obj.UserCreate);
                cmd.Parameters.AddWithValue("CreateDate", DateTime.Now.ToShortDateString());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }

        public void Update(clsClinicInformationList obj)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ClinicIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Region", obj.Region);
                cmd.Parameters.AddWithValue("ClinicName", obj.ClinicName);
                cmd.Parameters.AddWithValue("Description", obj.Description);
                cmd.Parameters.AddWithValue("URL", obj.URL);
                cmd.Parameters.AddWithValue("URLDisplay", obj.URLDisplay);
                cmd.Parameters.AddWithValue("UpdateUser", obj.UserCreate);
                cmd.Parameters.AddWithValue("UpdateDate", DateTime.Now.ToShortDateString());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }

        public void Delete(string pRegion,string pClinicName)
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ClinicDel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Region", pRegion);
                cmd.Parameters.AddWithValue("ClinicName", pClinicName);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
    }
}
