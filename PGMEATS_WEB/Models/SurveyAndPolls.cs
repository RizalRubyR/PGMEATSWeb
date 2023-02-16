using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGMEATS_WEB.Models
{
    public class SurveyAndPollsList
    {
        //public string User { get; set; }
        public string SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
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
    }
}