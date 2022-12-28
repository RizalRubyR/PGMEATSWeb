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
    public class VesselName
    {
        public string vesselname { get; set; } 
    }

    public class TripType
    {
        public string triptype { get; set; }
    }

    public class SoldTo
    {
        public string soldto { get; set; }
    }

    public class Method
    {
        public string methodcode { get; set; }
        public string methodname { get; set; }
    }

    public class ETD
    {
        public string etd { get; set; }
    }

    public class DistCode
    {
        public string distcode { get; set; }
    }

    public class CFC
    {
        public string cfc { get; set; }
    }

    public class Brand
    {
        public string brand { get; set; }
    }

    public class GeneralFilter
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }


    public class FilterDB
    {
        public IEnumerable<VesselName> VesselNameList()
        {
            try
            {
                List<VesselName> Vessels = new List<VesselName>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_VesselName_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        VesselName Vessel = new VesselName();
                        Vessel.vesselname = rd["VesselName"].ToString();
                        Vessels.Add(Vessel);
                    }
                    return Vessels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TripType> TripTypeList()
        {
            try
            {
                List<TripType> TripTypes = new List<TripType>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_TripType_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        TripType TripType = new TripType();
                        TripType.triptype = rd["TripType"].ToString();
                        TripTypes.Add(TripType);
                    }
                    return TripTypes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SoldTo> SoldToList()
        {
            try
            {
                List<SoldTo> SoldTos = new List<SoldTo>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_SoldTo_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        SoldTo SoldTo = new SoldTo();
                        SoldTo.soldto = rd["SoldTo"].ToString();
                        SoldTos.Add(SoldTo);
                    }
                    return SoldTos;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Method> MethodList()
        {
            try
            {
                List<Method> Methods = new List<Method>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_Method_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Method Method = new Method();
                        Method.methodcode = rd["MethodCode"].ToString();
                        Method.methodname = rd["MethodName"].ToString();
                        Methods.Add(Method);
                    }
                    return Methods;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ETD> ETDList()
        {
            try
            {
                List<ETD> ETDs = new List<ETD>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_ETD_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ETD ETD = new ETD();
                        ETD.etd = rd["ETD"].ToString();
                        ETDs.Add(ETD);
                    }
                    return ETDs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DistCode> DistCodeList()
        {
            try
            {
                List<DistCode> DistCodes = new List<DistCode>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_DistCode_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        DistCode DistCode = new DistCode();
                        DistCode.distcode = rd["DistCode"].ToString();
                        DistCodes.Add(DistCode);
                    }
                    return DistCodes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CFC> CFCList()
        {
            try
            {
                List<CFC> CFCs = new List<CFC>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_CFC_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        CFC CFC = new CFC();
                        CFC.cfc = rd["CFC"].ToString();
                        CFCs.Add(CFC);
                    }
                    return CFCs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Brand> BrandList()
        {
            try
            {
                List<Brand> Brands = new List<Brand>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_Brand_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Brand Brand = new Brand();
                        Brand.brand = rd["Brand"].ToString();
                        Brands.Add(Brand);
                    }
                    return Brands;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GeneralFilter> CompanyList()
        {
            try
            {
                List<GeneralFilter> GeneralFilters = new List<GeneralFilter>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_SJCompany_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        GeneralFilter GeneralFilter = new GeneralFilter();
                        GeneralFilter.Code = rd["CompanyName"].ToString();
                        GeneralFilter.Description = rd["CompanyName"].ToString();
                        GeneralFilters.Add(GeneralFilter);
                    }
                    return GeneralFilters;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GeneralFilter> PortOfLoadingList()
        {
            try
            {
                List<GeneralFilter> GeneralFilters = new List<GeneralFilter>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_PortOfLoading_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        GeneralFilter GeneralFilter = new GeneralFilter();
                        GeneralFilter.Code = rd["PortCode"].ToString();
                        GeneralFilter.Description = rd["PortName"].ToString();
                        GeneralFilters.Add(GeneralFilter);
                    }
                    return GeneralFilters;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GeneralFilter> SJDestList()
        {
            try
            {
                List<GeneralFilter> GeneralFilters = new List<GeneralFilter>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_SJDest_Sel";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        GeneralFilter GeneralFilter = new GeneralFilter();
                        GeneralFilter.Code = rd["SJDest"].ToString();
                        GeneralFilter.Description = rd["SJDest"].ToString();
                        GeneralFilters.Add(GeneralFilter);
                    }
                    return GeneralFilters;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GeneralFilter> KAPSJClaim(string Type)
        {
            try
            {
                List<GeneralFilter> GeneralFilters = new List<GeneralFilter>();
                string constr = ConfigurationManager.ConnectionStrings["DBCSApi"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "usp_KAPSJClaim_FillCombo";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Type", Type);
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();
                    int i = 0;
                    while (rd.Read())
                    {
                        GeneralFilter GeneralFilter = new GeneralFilter();
                        GeneralFilter.Code = (i + 1).ToString();
                        GeneralFilter.Description = rd["Description"].ToString();
                        GeneralFilters.Add(GeneralFilter);
                        i += 1;
                    }
                    return GeneralFilters;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}