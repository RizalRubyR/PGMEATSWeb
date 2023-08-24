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
using System.Data;
using System.Collections;

namespace PGMEATS_WEB.Controllers
{
    public class SurveyPollsListController : Controller
    {
        // GET: SurveyPollsList
        public ActionResult Index()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "C-01";

            clsUserPrivilegeDB db = new clsUserPrivilegeDB();
            clsUserPrivilege data = new clsUserPrivilege();
            data.UserID = userID;
            data.MenuID = MenuID;
            data.AdminStatus = AdminStatus;

            /*CHECK PRIVILEGE*/
            clsUserPrivilege Privilege = db.UserPrivilegeCheck("3", data);
            if (Privilege.AllowAccess == "0")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.AllowUpdate = Privilege.AllowUpdate;
            ViewBag.UserID = userID;

            return View();
        }

        public ActionResult Create()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "C-02";

            clsUserPrivilegeDB db = new clsUserPrivilegeDB();
            clsUserPrivilege data = new clsUserPrivilege();
            data.UserID = userID;
            data.MenuID = MenuID;
            data.AdminStatus = AdminStatus;

            /*CHECK PRIVILEGE*/
            clsUserPrivilege Privilege = db.UserPrivilegeCheck("3", data);
            if (Privilege.AllowAccess == "0")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.AllowUpdate = Privilege.AllowUpdate;
            ViewBag.UserID = userID;

            return View();
        }

        public ActionResult Update(string id)
        {
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }
            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "C-03";
            clsResponse Response = new clsResponse();
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            Response = db.getDataEdit(id);

            List<LoadEditSurvey> data = new List<LoadEditSurvey>();
            data = ((IEnumerable)Response.Contents).Cast<LoadEditSurvey>().ToList();

            for(int i = 0; i < data.Count; i++)
            {
                ViewBag.SurveyID = data[i].SurveyID;
                ViewBag.SurveyDesc = data[i].SurveyDesc;
                ViewBag.GroupDepartment = data[i].GroupDepartment;
                ViewBag.StartDate = data[i].StartDate;
                ViewBag.EndDate = data[i].EndDate;
                ViewBag.ViewResult = data[i].ViewResult;
                ViewBag.Type = data[i].Type;
            }

            ViewBag.SurveyID = id;
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult SurveyAndPolls(SurveyAndPollsListSearch param)
        {
            string userID = Session["LogUserID"].ToString();
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetSurveyAndPollsList(param, userID);
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

        public JsonResult LoadEditHeader(string surveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.getDataEdit(surveyID);
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

        [AcceptVerbs("GET","POST")]
        [HttpPost]
        public JsonResult fillDepartment()
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.fillDepartment();
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
        public JsonResult fillDesignation()
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.fillDesignation();
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

            clsResponse resp = db.validateQuestion(param.SurveyID, param.QuestionID);
            
            if(resp.ID == 1)
            {
                DataTable dtresp = (DataTable)resp.Contents;
                if (dtresp.Rows.Count > 0)
                {
                    response.ID = 0;
                    response.Message = "Question ID already exists";
                    response.Contents = "";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }

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
                            p.AnswerDesc = param2.txtmlt3;
                        }
                        else if (i == 3)
                        {
                            p.AnswerDesc = param2.txtmlt4;
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
                    p.AnswerDesc = "";
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

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public ActionResult UpdateDetail(string id,SurveyAndPollsDetail param, SurveyAndPollsAnswer param2, SurveyAndPollsHeader param3)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();


            string UserLogin = Session["LogUserID"].ToString().Trim();
            param.QuestionSeqNo = db.getQuestionSeqNo(param.SurveyID, param.QuestionID);
            try
            {
                List<surveyAnswer> cls = new List<surveyAnswer>();
                if (param.AnswerType == "0")
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
                        }
                        else if (i == 1)
                        {
                            p.AnswerDesc = param2.txtmlt2;
                        }
                        else if (i == 2)
                        {
                            p.AnswerDesc = param2.txtmlt3;
                        }
                        else if (i == 3)
                        {
                            p.AnswerDesc = param2.txtmlt4;
                        }
                        cls.Add(p);
                    }
                }
                else if (param.AnswerType == "1")
                {
                    surveyAnswer p = new surveyAnswer();
                    p.SurveyID = param2.SurveyID;
                    p.QuestionID = param2.QuestionID;
                    p.AnswerSeqNo = "1";
                    p.AnswerDesc = param2.txtFreeText;
                    cls.Add(p);
                }


                response = db.updateDetailandAnswer(id,param, cls,param3, UserLogin);
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
        public ActionResult Finalized(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            SurveyAndPollsHeader header = new SurveyAndPollsHeader();
            header.SurveyID = SurveyID;
            header.Finalized = "1";
            try
            {
                response = db.Finalized(header);
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
        public ActionResult Delete(string param)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.delete(param);
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSurvey(string param)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.DeleteSurvey(param);
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
        public ActionResult Load_Edit(string param)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.getEdit(param);
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