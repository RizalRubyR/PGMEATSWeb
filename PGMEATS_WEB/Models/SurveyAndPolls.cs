using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.IO;

namespace PGMEATS_WEB.Models
{
    public class SurveyAndPollsHeader
    {
        public string SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        public List<string> GroupDepartment { get; set; }
        public List<string> Designation { get; set; }
        public string SurveyStatus { get; set; } //0 = New, 1 = On Progress, 2 = Finish  update from mobile 
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ViewChart { get; set; }
        public string Type { get; set; }
        public string Finalized { get;set; } // 0 = New, 1 = Finalized update from web
    }

    public class SurveyAndPollsListSearch
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Groupdepartment { get; set; }
        public string Designation { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class SurveyAndPollsList
    {
        //public string User { get; set; }
        public string SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string SurveyStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreateDate { get; set; }
    }
    public class SurveyAndPollsDetailList
    {
        public string ID { get; set; }
        public string QuestionID { get; set; }
        public string SurveyID { get; set; }
        public string QuestionSeqNo { get; set; }
        public string QuestionDesc { get; set; }
        public string ParentQuestionID { get; set; }
        public string ParentAnswerSeqNo { get; set; }
    }
    public class SurveyAndPollsCreate
    {
        public string id { get; set; }
        public string QuestionID { get; set; }
        public string SurveyID { get; set; }
        public string QuestionSeqNo { get; set; }
        public string QuestionDesc { get; set; }
        public string ParentQuestionID { get; set; }
        public string ParentAnswerSeqNo { get; set; }
    }

    public class SurveyAndPollsDetail
    {
        public string QuestionID { get; set; }
        public string SurveyID { get; set; }
        public string QuestionSeqNo { get; set; }
        public string QuestionDesc { get; set; }
        public string ParentQuestionID { get; set; }
        public string ParentAnswerSeqNo { get; set; }
        public string AnswerType { get; set; }
    }

    public class SurveyAndPollsAnswer
    {
        public string SurveyID { get; set; }
        public string QuestionID { get; set; }
        public string AnswerSeqNo { get; set; }
        //public string AnswerDesc { get; set; }

        public string txtFreeText { get; set; }

        public string txtmlt1 { get; set; }
        public string txtmlt2 { get; set; }
        public string txtmlt3 { get; set; }
        public string txtmlt4 { get; set; }
    }

    public class surveyAnswer
    {
        public string SurveyID { get; set; }
        public string QuestionID { get; set; }
        public string AnswerSeqNo { get; set; }
        public string AnswerDesc { get; set; }
    }

    public class loadEdit
    {
        public string QuestionID { get; set; }
        public string QuestionDesc { get; set; }
        public string AnswerType { get; set; }
        public string ParentQuestionID { get; set; }
        public string ParentAnswerSeqNo { get; set; }
        public string AnswerSeqNo { get; set; }
        public string AnswerDesc { get; set; }

    }
    public class LoadEditSurvey
    {
        public string SurveyID { get; set; }
        public string SurveyDesc { get; set; }
        public string GroupDepartment { get; set; }
        public string Designation { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ViewResult { get; set; }
        public string Type { get; set; }
        public string Finalized { get; set; }

    }

    public class CheckResult
    {
        public int SurveyID { get; set; }
        public int ViewChart { get; set; }
    }

    public class headerEmplyee
    {
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionDesc { get; set; }
        public int ViewChart { get; set; }
    }

    public class employee
    {
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionDesc { get; set; }
        public int AnswerSeqNo { get; set; }
        public string AnswerDesc { get; set; }
        public int Sub_total { get; set; }
        public int Total { get; set; }
    }
    public class dept
    {
        public string Department { get; set; }
		public int SurveyID { get; set; }
        public string AnserDesc { get; set; }
        public int Total { get; set; }
        public int LastCol { get; set; }
    }
    public class shift
    {
        public string Shift { get; set; }
        public int SurveyID { get; set; }
        public string AnserDesc { get; set; }
        public int Total { get; set; }
        public int LastCol { get; set; }
    }

    public class Base64ToImage
	{
        public string nameID { get; set; }
        public string src { get; set; }
    }

    public class label
    {
        public int SurveyID { get; set; }
        public string AnswerDesc { get; set; }
        public int LastCol { get; set; }
    }

    public class checkResult
	{
        public int ID { get; set; }
        public string Messages { get; set; }
	}

    public class SurveyAndPollsDB
    {
        public clsResponse GetSurveyAndPollsList(SurveyAndPollsListSearch data, string user)
        {
            List<SurveyAndPollsList> SurveyPollsList = new List<SurveyAndPollsList>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SurveyAndPolls_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StartDate", data.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", data.EndDate);
                    cmd.Parameters.AddWithValue("@Groupdepartment", data.Groupdepartment);
                    cmd.Parameters.AddWithValue("@Designation", data.Designation);
                    cmd.Parameters.AddWithValue("@ActiveStatus", data.ActiveStatus);
                    cmd.Parameters.AddWithValue("@User", user);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    SurveyPollsList = dt.AsEnumerable().Select(x =>
                    new SurveyAndPollsList
                    {
                        SurveyID = x.Field<Int32>("SurveyID").ToString(),
                        SurveyTitle = x.Field<string>("SurveyTitle"),
                        Department = x.Field<string>("Department"),
                        Designation = x.Field<string>("Designation"),
                        SurveyStatus = x.Field<string>("SurveyStatus"),
                        StartDate = x.Field<string>("StartDate"),
                        EndDate = x.Field<string>("EndDate"),
                        CreateDate = x.Field<string>("CreateDate")
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = SurveyPollsList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse GetSurveyAndPollsCreate()
        {
            List<SurveyAndPollsCreate> SurveyPollsList = new List<SurveyAndPollsCreate>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SurveyAndPollsCreate_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    SurveyPollsList = dt.AsEnumerable().Select(x =>
                    new SurveyAndPollsCreate
                    {
                        id = x.Field<Int64>("id").ToString(),
                        QuestionID = x.Field<Int64>("QuestionID").ToString(),
                        SurveyID = x.Field<Int64>("SurveyID").ToString(),
                        QuestionSeqNo = x.Field<string>("QuestionSeqNo"),
                        QuestionDesc = x.Field<string>("QuestionDesc"),
                        ParentQuestionID = x.Field<string>("ParentQuestionID"),
                        ParentAnswerSeqNo = x.Field<string>("ParentAnswerSeqNo"),
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = SurveyPollsList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse getsurveyid()
        {
            int surveyid = 0;
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SurveyAndPollsCreate_SurveyID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    if(dt.Rows.Count > 0)
                    {
                        surveyid = int.Parse(dt.Rows[0]["SurveyID"].ToString()) + 1;
                    }
                    else
                    {
                        surveyid = 1;
                    }

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = surveyid;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse FillCombo(string type)
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SurveryAndPolls_FillCombo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Type", type);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("Code"),
                        Description = x.Field<string>("Description")
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse FillComboParentQuestion(string SurveyID)
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_parentQuestion_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("SurveyID", SurveyID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("QuestionID"),
                        Description = x.Field<string>("QuestionDesc")
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse fillDepartment()
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_M_SurveyAndPolls_Department_Load", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("GroupDepartment"),
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse fillDesignation()
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_M_SurveyAndPolls_Designation_Load", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("Designation"),
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse Finalized(SurveyAndPollsHeader header)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("execute sp_Surveyandpolls_CheckHeader @SurveyID", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", header.SurveyID);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if(dt.Rows.Count <= 0)
                    {
                        Response.ID = 0;
                        Response.Message = "Data not exists";
                        Response.Contents = "";
                    }
                    else
                    {
                        cmd.CommandText = "execute sp_SurveyandpollsHeader_Finalized @SurveyID, @Finalized";  //new SqlCommand("sp_SurveyandpollsHeader_Finalized", con);
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SurveyID", header.SurveyID);
                        cmd.Parameters.AddWithValue("@Finalized", header.Finalized);

                        da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        da.Dispose();
                        cmd.Dispose();
                        con.Close();

                        Response.ID = 1;
                        Response.Message = "Success";
                        Response.Contents = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }
        public clsResponse FillComboParentAnswer(string SurveyID, string ParentQuestionID)
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_parentAnswer_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    cmd.Parameters.AddWithValue("@ParentQuestionID", ParentQuestionID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("AnswerSeqNo"),
                        Description = x.Field<string>("AnswerDesc")
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse FillAnswerType()
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_AnswerType_GET", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    ComboList = dt.AsEnumerable().Select(x =>
                    new clsFillCombo
                    {
                        Code = x.Field<string>("Code"),
                        Description = x.Field<string>("Description")
                    }).ToList();

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = ComboList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse getQuestionID(string param)
        {
            List<clsFillCombo> ComboList = new List<clsFillCombo>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_QuestionID_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", param);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();
                    string QuestionID;
                    if (dt.Rows.Count > 0)
                    {
                        QuestionID = dt.Rows[0]["QuestionID"].ToString();
                    }
                    else
                    {
                        QuestionID = "1";
                    }

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = QuestionID;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse delete(string id)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_detailandAnswer_del", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    int j = cmd.ExecuteNonQuery();

                    Response.ID = 1;
                    Response.Message = "Success delete data";
                    Response.Contents = "";

                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
            }

            return Response;
        }

        public clsResponse DeleteSurvey(string SurveyID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_SurveyAndPollS_Del", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);

                    int j = cmd.ExecuteNonQuery();

                    Response.ID = 1;
                    Response.Message = "Success delete Survey";
                    Response.Contents = "";

                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
            }

            return Response;
        }
        
        public clsResponse getEdit(string id)
        {
            clsResponse Response = new clsResponse();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_detailandanswer_get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    List<loadEdit> data = new List<loadEdit>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        loadEdit detail = new loadEdit();
                        detail.QuestionID = dr["QuestionID"].ToString().Trim();
                        detail.QuestionDesc = dr["QuestionDesc"].ToString().Trim();
                        detail.AnswerType = dr["AnswerType"].ToString().Trim();
                        detail.ParentQuestionID = dr["ParentQuestionID"].ToString().Trim();
                        detail.ParentAnswerSeqNo = dr["ParentAnswerSeqNo"].ToString().Trim();
                        detail.AnswerSeqNo = dr["AnswerSeqNo"].ToString().Trim();
                        detail.AnswerDesc = dr["AnswerDesc"].ToString().Trim();

                        data.Add(detail);
                    }
                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = data;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
            }

            return Response;
        }

        public clsResponse getDataEdit(string SurveyID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_Surveyandpolls_Edit_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    List<LoadEditSurvey> data = new List<LoadEditSurvey>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        LoadEditSurvey detail = new LoadEditSurvey();
                        detail.SurveyID = dr["SurveyID"].ToString().Trim();
                        detail.SurveyDesc = dr["SurveyTitle"].ToString().Trim();
                        detail.GroupDepartment = dr["Department"].ToString().Trim();
                        detail.Designation = dr["Designation"].ToString().Trim();
                        detail.StartDate = dr["StartDate"].ToString().Trim();
                        detail.EndDate = dr["EndDate"].ToString().Trim();
                        detail.ViewResult = dr["ViewChart"].ToString().Trim();
                        detail.Type = dr["Type"].ToString().Trim();
                        detail.Finalized = dr["Finalized"].ToString().Trim();

                        data.Add(detail);
                    }
                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = data;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
            }

            return Response;
        }

        public clsResponse Saveheader(SurveyAndPollsHeader param, string UserLogin)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    string sql = "execute sp_SurveyandpollsHeader_Ins @SurveyID,@SurveyTitle,@SurveyStatus,@StartDate,@EndDate,@ViewChart,@Type,@Finalized,@CreateUser";
                    SqlCommand cmd = new SqlCommand(sql, con, trans);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SurveyID", param.SurveyID);
                    cmd.Parameters.AddWithValue("@SurveyTitle", param.SurveyTitle);
                    cmd.Parameters.AddWithValue("@SurveyStatus", param.SurveyStatus);
                    cmd.Parameters.AddWithValue("@StartDate", param.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", param.EndDate);
                    cmd.Parameters.AddWithValue("@ViewChart", param.ViewChart);
                    cmd.Parameters.AddWithValue("@Type", param.Type);
                    cmd.Parameters.AddWithValue("@Finalized", param.Finalized);
                    cmd.Parameters.AddWithValue("@CreateUser", UserLogin);

                    int i;

                    i = cmd.ExecuteNonQuery();

                    if (i < 1)
                    {
                        trans.Rollback();
                        Response.ID = 0;
                        Response.Message = "No data update";
                        Response.Contents = "";
                    }
                    cmd.CommandText = "execute sp_SurveyandpollsDepartmentBySurveyID_Del @SuveyID";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SuveyID", param.SurveyID);
                    i = cmd.ExecuteNonQuery();

                    foreach (string str in param.GroupDepartment)
                    {
                        cmd.CommandText = "execute sp_SurveyandpollsDepartment_Ins @SuveyID,@GroupDepartment";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SuveyID", param.SurveyID);
                        cmd.Parameters.AddWithValue("@GroupDepartment", str);
                        i = cmd.ExecuteNonQuery();
                        if (i < 1)
                        {
                            trans.Rollback();
                            Response.ID = 0;
                            Response.Message = "Cannot insert department";
                            Response.Contents = "";
                            return Response;
                        }
                    }

                    cmd.CommandText = "execute sp_SurveyandpollsDesignationBySurveyID_Del @SuveyID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SuveyID", param.SurveyID);
                    i = cmd.ExecuteNonQuery();

                    foreach (string str in param.Designation)
                    {
                        cmd.CommandText = "execute sp_SurveyandpollsDesignation_Ins @SuveyID,@Designation";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SuveyID", param.SurveyID);
                        cmd.Parameters.AddWithValue("@Designation", str);
                        i = cmd.ExecuteNonQuery();
                        if (i < 1)
                        {
                            trans.Rollback();
                            Response.ID = 0;
                            Response.Message = "Cannot insert designation";
                            Response.Contents = "";
                            return Response;
                        }
                    }

                    //DataTable dt = new DataTable();
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(dt);
                    //da.Dispose();
                    //cmd.Dispose();
                    //con.Close();
                    trans.Commit();
                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = "";
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public clsResponse saveDetailandAnswer(SurveyAndPollsDetail param, List<surveyAnswer> param2, string UserLogin)
        {
            clsResponse Response = new clsResponse();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_SurveyAndPollS_Detail_Ins_Validate",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", param.SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionID", param.QuestionID);
                    cmd.Parameters.AddWithValue("@ParentQID", param.ParentQuestionID);
                    cmd.Parameters.AddWithValue("@ParentAns", param.ParentAnswerSeqNo);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Response.ID = 0;
                        Response.Message = "Parent question and parent answer already exist";
                        Response.Contents = "";
                        return Response;
                    }

                    cmd = new SqlCommand("sp_SurveyAndPollS_Detail_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuestionID", param.QuestionID);
                    cmd.Parameters.AddWithValue("@SurveyID", param.SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionSeqNo", param.QuestionSeqNo);
                    cmd.Parameters.AddWithValue("@QuestionDesc", param.QuestionDesc);
                    cmd.Parameters.AddWithValue("@ParentQuestionID", param.ParentQuestionID);
                    cmd.Parameters.AddWithValue("@ParentAnswerSeqNo", param.ParentAnswerSeqNo);
                    cmd.Parameters.AddWithValue("@AnswerType", param.AnswerType);
                    dt = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    cmd = new SqlCommand("sp_SurveyAndPollS_Answer_Ins", con);
                    for (int i = 0; i < param2.Count; i++){
                        if (param2[i].AnswerDesc != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SurveyID", param2[i].SurveyID);
                            cmd.Parameters.AddWithValue("@QuestionID", param2[i].QuestionID);
                            cmd.Parameters.AddWithValue("@AnswerSeqNo", param2[i].AnswerSeqNo);
                            cmd.Parameters.AddWithValue("@AnswerDesc", param2[i].AnswerDesc);
                            int j = cmd.ExecuteNonQuery();
                        }
                    }

                    Response.ID = 1;
                    Response.Message = "Success submit data";
                    Response.Contents = "";

                }
            }
            catch(Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
            }

            return Response;
        }

        public clsResponse updateDetailandAnswer(string id,SurveyAndPollsDetail param, List<surveyAnswer> param2, SurveyAndPollsHeader param3,string UserLogin)
        {
            clsResponse Response = new clsResponse();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;

                    cmd = new SqlCommand("sp_SurveyandpollsHeader_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", param3.SurveyID);
                    cmd.Parameters.AddWithValue("@SurveyTitle", param3.SurveyTitle);
                    cmd.Parameters.AddWithValue("@StartDate", param3.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", param3.EndDate);
                    cmd.Parameters.AddWithValue("@ViewChart", param3.ViewChart);
                    cmd.Parameters.AddWithValue("Type", param3.Type);
                    cmd.Parameters.AddWithValue("@CreateUser", UserLogin);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    cmd = new SqlCommand("sp_SurveyAndPollS_Detail_Upd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@QuestionID", param.QuestionID);
                    cmd.Parameters.AddWithValue("@SurveyID", param.SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionSeqNo", param.QuestionSeqNo);
                    cmd.Parameters.AddWithValue("@QuestionDesc", param.QuestionDesc);
                    cmd.Parameters.AddWithValue("@ParentQuestionID", param.ParentQuestionID);
                    cmd.Parameters.AddWithValue("@ParentAnswerSeqNo", param.ParentAnswerSeqNo);
                    cmd.Parameters.AddWithValue("@AnswerType", param.AnswerType);
                    dt = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    cmd = new SqlCommand("sp_SurveyAndPollS_Answer_Upd", con);
                    for (int i = 0; i < param2.Count; i++)
                    {
                        if(param2[i].AnswerDesc != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SurveyID", param2[i].SurveyID);
                            cmd.Parameters.AddWithValue("@QuestionID", param2[i].QuestionID);
                            cmd.Parameters.AddWithValue("@AnswerSeqNo", param2[i].AnswerSeqNo);
                            cmd.Parameters.AddWithValue("@AnswerDesc", param2[i].AnswerDesc);
                            int j = cmd.ExecuteNonQuery();
                        }
                    }

                    Response.ID = 1;
                    Response.Message = "Success submit data";
                    Response.Contents = "";

                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";
            }

            return Response;
        }

        public string getAnswerSeqNo(string SurveyID, string QuestionID)
        {
            string SeqNo = "";

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_AnswerSeqNo_GET", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        SeqNo = dt.Rows[0]["SeqNo"].ToString();
                    }
                    else
                    {
                        SeqNo = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                SeqNo = null;
            }

            return SeqNo;

        }

        public string getQuestionSeqNo(string SurveyID, string ParentQID)
        {
            string SeqNo = "";
            //sp_QuestionSeqNo_Get
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_QuestionSeqNo_Get", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    cmd.Parameters.AddWithValue("@ParentQID", ParentQID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        SeqNo = dt.Rows[0]["SeqNo"].ToString();
                    }
                    else
                    {
                        SeqNo = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                SeqNo = null;
            }

            return SeqNo;
        }

        public clsResponse validateQuestion(string SurveyID, string QuestionID)
        {

            DataTable dt = new DataTable();
            clsResponse resp = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_SurveyandPolls_CheckQuestion", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);

                    //DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    resp.ID = 1;
                    resp.Message = "Success";
                    resp.Contents = dt;
                }
            }
            catch (Exception ex)
            {
                resp.ID = 0;
                resp.Message = ex.Message;
                resp.Contents = dt;
            }
            return resp;
        }

        public clsResponse validateParent(string SurveyID, string QuestionSeqNo, string ParentQuestionID, string ParentAnswerSeqNo)
        {
            DataTable dt = new DataTable();
            clsResponse resp = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
                    cmd = new SqlCommand("sp_SurveyandPolls_CheckParent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionSeqNo", QuestionSeqNo);
                    cmd.Parameters.AddWithValue("@ParentQuestionID", ParentQuestionID);
                    cmd.Parameters.AddWithValue("@ParentAnswerSeqNo", ParentAnswerSeqNo);

                    //DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    resp.ID = 1;
                    resp.Message = "Success";
                    resp.Contents = dt;
                }
            } catch(Exception ex)
            {
                resp.ID = 0;
                resp.Message = ex.Message;
                resp.Contents = dt;
            }
            return resp;
        }

        public clsResponse GetSurveyAndPollsDetailList(string param)
        {
            List<SurveyAndPollsDetailList> SurveyPollsDetailList = new List<SurveyAndPollsDetailList>();
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("NEW_sp_SurveyAndPollsDetail_List", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@surveyID", param);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    foreach(DataRow dr in dt.Rows)
                    {
                        SurveyAndPollsDetailList data = new SurveyAndPollsDetailList();
                        data.ID = dr["id"].ToString().Trim();
                        data.QuestionID = dr["QuestionID"].ToString();
                        data.SurveyID = dr["SurveyID"].ToString();
                        data.QuestionSeqNo = dr["QuestionSeqNo"].ToString();
                        data.QuestionDesc = dr["QuestionDesc"].ToString();
                        data.ParentQuestionID = dr["ParentQuestionID"].ToString();
                        data.ParentAnswerSeqNo = dr["ParentAnswerSeqNo"].ToString();

                        SurveyPollsDetailList.Add(data);
                    }

                    Response.ID = 1;
                    Response.Message = "Success";
                    Response.Contents = SurveyPollsDetailList;
                }
            }
            catch (Exception ex)
            {
                Response.ID = 0;
                Response.Message = ex.Message;
                Response.Contents = "";

            }
            return Response;
        }

        public List<CheckResult> ResultCheck(string id)
        {
            List<CheckResult> res = new List<CheckResult>();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SurveyAndPollsDetail_ResultCheck", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@surveyID", id);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();


                    List<CheckResult> listObject = dt.AsEnumerable().Select(x => new CheckResult(){
                        SurveyID = Convert.ToInt16(x.Field<object>("SurveyID")),
                        ViewChart = Convert.ToInt16(x.Field<object>("ViewChart"))
                    }).ToList();

                    res = listObject;
                }
            }
            catch(Exception ex)
            {
                
            }
            return res;
        }

        public IEnumerable<clsResponse> GetchartHeaderByEmployee(string param)
        {
            List<clsResponse> responList = new List<clsResponse>();
            clsResponse clsrespon = new clsResponse();
            Encryption encrypt = new Encryption();
            try
            {
                var array = encrypt.DecryptData(param).Split(new string[] { "||" }, StringSplitOptions.None);
                string surveyID = array[0];
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_Surveyandpoolschart_header_get";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@surveyID", surveyID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    List<headerEmplyee> head = dt.AsEnumerable().Select(x => new headerEmplyee()
                    {
                        SurveyID = Convert.ToInt16(x.Field<object>("SurveyID")),
                        QuestionID = Convert.ToInt16(x.Field<object>("QuestionID")),
                        QuestionDesc = Convert.ToString(x.Field<object>("QuestionDesc")),
                        ViewChart = Convert.ToInt16(x.Field<object>("ViewChart"))
                    }).ToList();

                    clsrespon = new clsResponse();
                    clsrespon.ID = 0;
                    clsrespon.Message = "Success";
                    clsrespon.Contents = head;

                    responList.Add(clsrespon);
                }
            }
            catch (Exception ex)
            {
                responList = new List<clsResponse>();
                clsrespon = new clsResponse();

                clsrespon.ID = 1;
                clsrespon.Message = ex.Message;
                clsrespon.Contents = "";

                responList.Add(clsrespon);
            }
            return responList;
        }

        public IEnumerable<clsResponse> GetchartByEmployee(string param)
        {
            List<clsResponse> responList = new List<clsResponse>();
            clsResponse clsrespon = new clsResponse();
            Encryption encrypt = new Encryption();
            try
            {
                var array = encrypt.DecryptData(param).Split(new string[] { "||" }, StringSplitOptions.None);
                string surveyID = array[0];
                string questionID = array[1];
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_Surveyandpoolschart_get";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@surveyID", surveyID);
                    cmd.Parameters.AddWithValue("@questionID", questionID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    List<employee> emp = dt.AsEnumerable().Select(x => new employee()
                    {
                        SurveyID = Convert.ToInt16(x.Field<object>("SurveyID")),
                        QuestionID = Convert.ToInt16(x.Field<object>("QuestionID")),
                        QuestionDesc = Convert.ToString(x.Field<object>("QuestionDesc")),
                        AnswerSeqNo = Convert.ToInt16(x.Field<object>("AnswerSeqNo")),
                        AnswerDesc = Convert.ToString(x.Field<object>("AnswerDesc")),
                        Sub_total = Convert.ToInt16(x.Field<object>("Sub_total")),
                        Total = Convert.ToInt16(x.Field<object>("Total"))
                    }).ToList();

                    clsrespon = new clsResponse();
                    clsrespon.ID = 0;
                    clsrespon.Message = "Success";
                    clsrespon.Contents = emp;

                    responList.Add(clsrespon);
                }
            }
            catch (Exception ex)
            {
                responList = new List<clsResponse>();
                clsrespon = new clsResponse();

                clsrespon.ID = 1;
                clsrespon.Message = ex.Message;
                clsrespon.Contents = "";

                responList.Add(clsrespon);
            }
            return responList;
        }

        public IEnumerable<clsResponse> GetchartByDepartment(string param)
        {
            List<clsResponse> responList = new List<clsResponse>();
            clsResponse clsrespon = new clsResponse();
            Encryption encrypt = new Encryption();
            try
            {
                var array = encrypt.DecryptData(param).Split(new string[] { "||" }, StringSplitOptions.None);
                string surveyID = array[0];
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_SurveyPollsResult_ByDepartment";  //"sp_surveyandpoolsbydept_get";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", surveyID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    //List<dept> dept = dt.AsEnumerable().Select(x => new dept() {
                    //    Department = Convert.ToString(x.Field<object>("Department")),
                    //    SurveyID = Convert.ToInt16(x.Field<object>("SurveyID")),
                    //    AnserDesc = Convert.ToString(x.Field<object>("AnserDesc")),
                    //    Total = Convert.ToInt16(x.Field<object>("Total")),
                    //    LastCol = Convert.ToInt16(x.Field<object>("LastCol"))
                    //}).ToList();

                    

                    clsrespon = new clsResponse();
                    clsrespon.ID = 0;
                    clsrespon.Message = "Success";
                    clsrespon.Contents = dt.AsEnumerable().Select(row => row.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col])).Select(dict => (dynamic)dict).ToList();

                    responList.Add(clsrespon);
                }
            }
            catch (Exception ex)
            {
                responList = new List<clsResponse>();
                clsrespon = new clsResponse();

                clsrespon.ID = 1;
                clsrespon.Message = ex.Message;
                clsrespon.Contents = "";

                responList.Add(clsrespon);
            }
            return responList;
        }

        public IEnumerable<clsResponse> GetchartByShift(string param)
        {
            List<clsResponse> responList = new List<clsResponse>();
            clsResponse clsrespon = new clsResponse();
            Encryption encrypt = new Encryption();
            try
            {
                var array = encrypt.DecryptData(param).Split(new string[] { "||" }, StringSplitOptions.None);
                string surveyID = array[0];
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_SurveyPollsResult_ByShift"; //"sp_surveyandpoolsbyshift_get";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", surveyID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    //List<shift> shift = dt.AsEnumerable().Select(x => new shift() {
                    //    Shift = Convert.ToString(x.Field<object>("Shift")),
                    //    SurveyID = Convert.ToInt16(x.Field<object>("SurveyID")),
                    //    AnserDesc = Convert.ToString(x.Field<object>("AnserDesc")),
                    //    Total = Convert.ToInt16(x.Field<object>("Total")),
                    //    LastCol = Convert.ToInt16(x.Field<object>("LastCol"))
                    //}).ToList();

                    clsrespon = new clsResponse();
                    clsrespon.ID = 0;
                    clsrespon.Message = "Success";
                    clsrespon.Contents = dt.AsEnumerable().Select(row => row.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row[col])).Select(dict => (dynamic)dict).ToList();

                    responList.Add(clsrespon);
                }
            }
            catch (Exception ex)
            {
                responList = new List<clsResponse>();
                clsrespon = new clsResponse();

                clsrespon.ID = 1;
                clsrespon.Message = ex.Message;
                clsrespon.Contents = "";

                responList.Add(clsrespon);
            }
            return responList;
        }

        public IEnumerable<clsResponse> Getlabelanswer(string param)
        {
            List<clsResponse> responList = new List<clsResponse>();
            clsResponse clsrespon = new clsResponse();
            Encryption encrypt = new Encryption();
            try
            {
                var array = encrypt.DecryptData(param).Split(new string[] { "||" }, StringSplitOptions.None);
                string surveyID = array[0];
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_SurveyandpoolLabel_Get";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", surveyID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    List<label> label = dt.AsEnumerable().Select( x => new label() {
                        SurveyID = Convert.ToInt16(x.Field<object>("SurveyID")),
                        AnswerDesc = Convert.ToString(x.Field<object>("AnswerDesc")),
                        LastCol = Convert.ToInt16(x.Field<object>("LastCol"))
                    }).ToList();

                    clsrespon = new clsResponse();
                    clsrespon.ID = 0;
                    clsrespon.Message = "Success";
                    clsrespon.Contents = label;

                    responList.Add(clsrespon);
                }
            }
            catch (Exception ex)
            {
                responList = new List<clsResponse>();
                clsrespon = new clsResponse();

                clsrespon.ID = 1;
                clsrespon.Message = ex.Message;
                clsrespon.Contents = "";

                responList.Add(clsrespon);
            }
            return responList;
        }

        public List<checkResult> CheckAnswer(string param)
        {
            List<checkResult> responList = new List<checkResult>();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string sql = "sp_SurveyPollsResult_Check";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", param);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

                    responList = dt.AsEnumerable().Select(x => new checkResult()
                    {
                        ID = Convert.ToInt16(x.Field<object>("ID")),
                        Messages = Convert.ToString(x.Field<object>("Message"))
                    }).ToList();

                }
            }
            catch (Exception ex)
            {
            }
            return responList;
        }

        public IEnumerable<clsResponse> Base64ToImg(List<Base64ToImage>  data)
        {
            List<clsResponse> responList = new List<clsResponse>();
            clsResponse clsrespon = new clsResponse();

            try
            {
                string d = DateTime.Now.ToString("yyyyMMdd");
                //string pathIIS = System.Web.HttpContext.Current.Server.MapPath("~");
                //string pathImg = "\\" + "Content" + "\\" + "img" + "\\" + "SurveyandPolls" + "\\" + d;
                //string pathFolder = pathIIS + pathImg;
                //if (!System.IO.Directory.Exists((pathFolder)))
                //{
                //    System.IO.Directory.CreateDirectory(pathFolder);
                //}

                for (int i = 0; i < data.Count; i++)
                {
                    //string base64image = data[i].src; //.Split("data:image/png;base64,")[0];
                    //var imageMatchesPNG = Regex.Match(data[i].src.ToString(), "(data:image/png;base64,)([^\"]*)");
                    //string Filename = data[i].nameID + ".png";
                    //string xfile = pathFolder + "\\" + Filename;
                    //File.WriteAllBytes(pathFolder + "\\" + Filename, Convert.FromBase64String(base64image));
                    //clsrespon = new clsResponse();
                    //clsrespon.ID = 0;
                    //clsrespon.Message = "Success";
                    //clsrespon.Contents = xfile;
                    //responList.Add(clsrespon);

                    string URL = data[0].src;
                    string Filename = data[i].nameID + ".png";
                    string pathIIS = System.Web.HttpContext.Current.Server.MapPath("~");
                    string pathImg = "\\" + "Content" + "\\" + "img" + "\\" + "SurveyandPolls" + "\\" + d;
                    string pathFolder = pathIIS + pathImg;

                    MatchCollection matches = Regex.Matches(URL, "(data:image/[^'\"]+;base64,[^'\"]+)");

                    // Otomatis akan Terbuat Folder tsb
                    if (!System.IO.Directory.Exists((pathFolder)))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }

                    for (int ci = 0; ci <= matches.Count - 1; ci++)
                    {
                        string base64image = "";
                        string guid = Convert.ToString(Guid.NewGuid());
                        guid = guid.Replace("-", "").Substring(0, 12).ToUpper();

                        //string Filename = guid + ".png";

                        var Text = matches[ci].Groups[0];
                        var discardText = matches[ci].Groups[1].ToString();

                        var imageMatchesJPEG = Regex.Match(discardText, "(data:image/jpeg;base64,)([^\"]*)");
                        var imageMatchesPNG = Regex.Match(discardText, "(data:image/png;base64,)([^\"]*)");
                        var imageMatches = imageMatchesJPEG.Groups.Count != 1 ? imageMatchesJPEG : imageMatchesPNG;

                        base64image = imageMatches.Groups[2].ToString();

                        File.WriteAllBytes(pathFolder + "\\" + Filename, Convert.FromBase64String(base64image));

                        URL = URL.Replace(imageMatches.Groups[0].ToString(), ConfigurationManager.AppSettings["URL"].ToString() + pathImg.Replace("\\", "/") + "/" + Filename);
                    }

                    clsrespon = new clsResponse();
                    clsrespon.ID = 0;
                    clsrespon.Message = "Success";
                    clsrespon.Contents = URL;

                    responList.Add(clsrespon);

                }
                //string URL = data[0].src;
                //string pathIIS = System.Web.HttpContext.Current.Server.MapPath("~");
                //string pathImg = "\\" + "Content" + "\\" + "img" + "\\" + "SurveyandPolls";
                //string pathFolder = pathIIS + pathImg;

                //MatchCollection matches = Regex.Matches(URL, "<img[^>]+?src=['\"](data:image/[^'\"]+;base64,[^'\"]+)['\"][^>]*>");

                //// Otomatis akan Terbuat Folder tsb
                //if (!System.IO.Directory.Exists((pathFolder)))
                //{
                //    System.IO.Directory.CreateDirectory(pathFolder);
                //}

                //for (int i = 0; i <= matches.Count - 1; i++)
                //{
                //    string base64image = "";
                //    string guid = Convert.ToString(Guid.NewGuid());
                //    guid = guid.Replace("-", "").Substring(0, 12).ToUpper();

                //    string Filename = guid + ".png";

                //    var Text = matches[i].Groups[0];
                //    var discardText = matches[i].Groups[1].ToString();

                //    var imageMatchesJPEG = Regex.Match(discardText, "(data:image/jpeg;base64,)([^\"]*)");
                //    var imageMatchesPNG = Regex.Match(discardText, "(data:image/png;base64,)([^\"]*)");
                //    var imageMatches = imageMatchesJPEG.Groups.Count != 1 ? imageMatchesJPEG : imageMatchesPNG;

                //    base64image = imageMatches.Groups[2].ToString();

                //    File.WriteAllBytes(pathFolder + "\\" + Filename, Convert.FromBase64String(base64image));

                //    URL = URL.Replace(imageMatches.Groups[0].ToString(), ConfigurationManager.AppSettings["URL"].ToString() + pathImg.Replace("\\", "/") + "/" + Filename);
                //}

                //clsrespon = new clsResponse();
                //clsrespon.ID = 0;
                //clsrespon.Message = "Success";
                //clsrespon.Contents = URL;

                //responList.Add(clsrespon);
            }
            catch (Exception ex)
            {
                responList = new List<clsResponse>();
                clsrespon = new clsResponse();

                clsrespon.ID = 1;
                clsrespon.Message = ex.Message;
                clsrespon.Contents = "";

                responList.Add(clsrespon);
            }
            return responList;
        }
    }
}