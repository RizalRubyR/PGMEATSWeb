using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PGMEATS_WEB.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace PGMEATS_WEB.Controllers
{
    public class SurveyPollsListController : Controller
    {
        // GET: SurveyPollsList
        public ActionResult Index()
        {
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserID = Session["LogUserID"];
            return View();
        }

        public ActionResult Create()
        {
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult SurveyAndPolls()
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetSurveyAndPollsList();
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SurveyAndPollsCreate()
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetSurveyAndPollsCreate();
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getsurveyid()
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.getsurveyid();
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SurveyAndPollsDetail(string param)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetSurveyAndPollsDetailList(param);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult FillCombo(string Type)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.FillCombo(Type);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult FillParentQuestion(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.FillComboParentQuestion(SurveyID);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillComboParentAnswer(string SurveyID, string ParentQuestionID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.FillComboParentAnswer(SurveyID, ParentQuestionID);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult FillAnswerType()
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.FillAnswerType();
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult getQuestionID(string param)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.getQuestionID(param);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public ActionResult SaveHeader(SurveyAndPollsHeader param)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            DateTime dtStart = DateTime.Parse(param.StartDate);
            DateTime dtEnd = DateTime.Parse(param.EndDate);

            param.StartDate = dtStart.ToString("yyyy-MM-dd");
            param.EndDate = dtEnd.ToString("yyyy-MM-dd");
            string UserLogin = Session["LogUserID"].ToString().Trim();
            try
            {
                response = db.Saveheader(param, UserLogin);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public ActionResult SaveDetail(SurveyAndPollsDetail param, SurveyAndPollsAnswer param2)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();


            string UserLogin = Session["LogUserID"].ToString().Trim();
            param.QuestionSeqNo = db.getQuestionSeqNo(param.SurveyID, param.ParentQuestionID);
            try
            {
                List<surveyAnswer> cls = new List<surveyAnswer>();
                if(param.AnswerType =="0")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        surveyAnswer p = new surveyAnswer();
                        p.SurveyID = param2.SurveyID;
                        p.QuestionID = param2.QuestionID;
                        p.AnswerSeqNo = (i + 1).ToString();
                        if (i == 0)
                        {
                            p.AnswerDesc = param2.txtmlt1;
                        }else if (i == 1)
                        {
                            p.AnswerDesc = param2.txtmlt2;
                        }
                        else if (i == 2)
                        {
                            p.AnswerDesc = param2.txtmlt2;
                        }
                        else if (i == 3)
                        {
                            p.AnswerDesc = param2.txtmlt3;
                        }
                        cls.Add(p);
                    }
                }
                else if(param.AnswerType == "1")
                {
                    surveyAnswer p = new surveyAnswer();
                    p.SurveyID = param2.SurveyID;
                    p.QuestionID = param2.QuestionID;
                    p.AnswerSeqNo = "1";
                    p.AnswerDesc = param2.txtFreeText;
                    cls.Add(p);
                }


                response = db.saveDetailandAnswer(param, cls, UserLogin);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}