﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class SurveyAndPollsHeader
    {
        public string SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyStatus { get; set; } //0 = New, 1 = On Progress, 2 = Finish  update from mobile 
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ViewChart { get; set; }

    }
    public class SurveyAndPollsList
    {
        //public string User { get; set; }
        public string SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
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
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ViewResult { get; set; }
    }

    public class SurveyAndPollsDB
    {
        public clsResponse GetSurveyAndPollsList()
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
                        SurveyID = x.Field<Int64>("SurveyID").ToString(),
                        SurveyTitle = x.Field<string>("SurveyTitle"),
                        SurveyStatus = x.Field<string>("SurveyStatus"),
                        StartDate = x.Field<string>("StartDate"),
                        EndDate = x.Field<string>("EndDate"),
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
                    SqlCommand cmd = new SqlCommand("sp_FilterCombo", con);
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
        public clsResponse Finalized(string SurveyID)
        {
            clsResponse Response = new clsResponse();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("sp_SurveyandpollsHeader_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", SurveyID);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

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
                        detail.StartDate = dr["StartDate"].ToString().Trim();
                        detail.EndDate = dr["EndDate"].ToString().Trim();
                        detail.ViewResult = dr["ViewChart"].ToString().Trim();

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
                    SqlCommand cmd = new SqlCommand("sp_SurveyandpollsHeader_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurveyID", param.SurveyID);
                    cmd.Parameters.AddWithValue("@SurveyTitle", param.SurveyTitle);
                    cmd.Parameters.AddWithValue("@SurveyStatus", param.SurveyStatus);
                    cmd.Parameters.AddWithValue("@StartDate", param.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", param.EndDate);
                    cmd.Parameters.AddWithValue("@ViewChart", param.ViewChart);
                    cmd.Parameters.AddWithValue("@CreateUser", UserLogin);
                    con.Open();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();

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
                    cmd = new SqlCommand("sp_SurveyAndPollS_Detail_Ins", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuestionID", param.QuestionID);
                    cmd.Parameters.AddWithValue("@SurveyID", param.SurveyID);
                    cmd.Parameters.AddWithValue("@QuestionSeqNo", param.QuestionSeqNo);
                    cmd.Parameters.AddWithValue("@QuestionDesc", param.QuestionDesc);
                    cmd.Parameters.AddWithValue("@ParentQuestionID", param.ParentQuestionID);
                    cmd.Parameters.AddWithValue("@ParentAnswerSeqNo", param.ParentAnswerSeqNo);
                    cmd.Parameters.AddWithValue("@AnswerType", param.AnswerType);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    cmd = new SqlCommand("sp_SurveyAndPollS_Answer_Ins", con);
                    for (int i = 0; i < param2.Count; i++){
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SurveyID", param2[i].SurveyID);
                        cmd.Parameters.AddWithValue("@QuestionID", param2[i].QuestionID);
                        cmd.Parameters.AddWithValue("@AnswerSeqNo", param2[i].AnswerSeqNo);
                        cmd.Parameters.AddWithValue("@AnswerDesc", param2[i].AnswerDesc);
                        int j = cmd.ExecuteNonQuery();
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

        public clsResponse updateDetailandAnswer(string id,SurveyAndPollsDetail param, List<surveyAnswer> param2, string UserLogin)
        {
            clsResponse Response = new clsResponse();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd;
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
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    //cmd = new SqlCommand("sp_SurveyAndPollS_Answer_Upd", con);
                    //for (int i = 0; i < param2.Count; i++)
                    //{
                    //    cmd.Parameters.Clear();
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("@SurveyID", param2[i].SurveyID);
                    //    cmd.Parameters.AddWithValue("@QuestionID", param2[i].QuestionID);
                    //    cmd.Parameters.AddWithValue("@AnswerSeqNo", param2[i].AnswerSeqNo);
                    //    cmd.Parameters.AddWithValue("@AnswerDesc", param2[i].AnswerDesc);
                    //    int j = cmd.ExecuteNonQuery();
                    //}

                    //Response.ID = 1;
                    //Response.Message = "Success submit data";
                    //Response.Contents = "";

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

                    //SurveyPollsDetailList = dt.AsEnumerable().Select(x =>
                    //new SurveyAndPollsDetailList
                    //{
                    //    ID = x.Field<Int32>("id").ToString(),
                    //    QuestionID = x.Field<Int32>("QuestionID").ToString(),
                    //    SurveyID = x.Field<Int32>("SurveyID").ToString(),
                    //    QuestionSeqNo = x.Field<Int64>("QuestionSeqNo").ToString(),
                    //    QuestionDesc = x.Field<string>("QuestionDesc").ToString(),
                    //    ParentQuestionID = x.Field<string>("ParentQuestionID").ToString(),
                    //    ParentAnswerSeqNo = x.Field<string>("ParentAnswerSeqNo").ToString(),
                    //}).ToList();

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
    }
}