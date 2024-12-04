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
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace PGMEATS_WEB.Controllers
{
    public class SurveyPollsListController : Controller
    {
        // GET: SurveyPollsList
        public ActionResult Index(string back)
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["StringBack"] != null)
            {
                ViewBag.stringBack = Session["StringBack"].ToString();
                Session["StringBack"] = null;
			}
			else
			{
                ViewBag.stringBack = "";

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

        public ActionResult Result(string id)
        {
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            List<CheckResult> resp = new List<CheckResult>();
            List<checkResult> check = new List<checkResult>();
            List<checkResult> Responses = new List<checkResult>();
            SurveyAndPollsDB db = new SurveyAndPollsDB();

            resp = db.ResultCheck(id);

            check = db.CheckAnswer(id);

            Responses = db.GetResponses(id);

            List<SurveyAndPollsHeader> resultInfo = new List<SurveyAndPollsHeader>();
            resultInfo = db.GetInfo(id);

            ViewBag.SurveyID = id;
            ViewBag.ViewChart = resp[0].ViewChart;
            ViewBag.AnswerCheck = check[0].Messages;

            ViewBag.SurveyTitle = resultInfo[0].SurveyTitle;
            ViewBag.InfoUser = resultInfo[0].CreateBy + " | " + DateTime.Now.ToString("dd MMM yyyy");

            ViewBag.Responses = Responses[0].Messages; //"50 Responses";
            Session["SurveyTitle"] = resultInfo[0].SurveyTitle;
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
                string stringBack = DateTime.Parse(param.StartDate).ToString("dd MMM yyyy") + "|" + DateTime.Parse(param.EndDate).ToString("dd MMM yyyy") + " | " + param.Groupdepartment + "|" + param.Designation + "|" + param.ActiveStatus;
                Session["StringBack"] = stringBack;
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
        public ActionResult CopyDetail(string SurveyIDFrom, string SurveyIDTo)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            clsResponse response = new clsResponse();

            try
            {
                response = db.CopyDataDetail(SurveyIDFrom, SurveyIDTo);

            }
            catch(Exception ex)
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
            clsResponse vParent = db.validateParent(param.SurveyID, param.QuestionSeqNo, param.ParentQuestionID, param.ParentAnswerSeqNo);
            if(vParent.ID == 1)
            {
                DataTable dtvParent = (DataTable)vParent.Contents;
                if(dtvParent.Rows.Count > 0)
                {
                    response.ID = 0;
                    response.Message = "Parent question and parent answer already exists";
                    response.Contents = "";
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
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
                else if (param.AnswerType == "2")
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

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public ActionResult getchartheaderbyemployee(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.GetchartHeaderByEmployee(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs("GET", "POST")]
        //[HttpPost]
        //public ActionResult ExportExcel()
        //{
        //    //var products = new System.Data.DataTable("teste");
        //    //products.Columns.Add("col1", typeof(int));
        //    //products.Columns.Add("col2", typeof(string));

        //    //products.Rows.Add(1, "product 1");
        //    //products.Rows.Add(2, "product 2");
        //    //products.Rows.Add(3, "product 3");
        //    //products.Rows.Add(4, "product 4");
        //    //products.Rows.Add(5, "product 5");
        //    //products.Rows.Add(6, "product 6");
        //    //products.Rows.Add(7, "product 7");


        //    //return File(Encoding.ASCII.GetBytes(), "application/vnd.ms-excel", "Grid.xls");
        //}

        //[AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult getchartbyemployee(string SurveyID, string QuestionID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||" + QuestionID + "||");
            try
            {
                response = db.GetchartByEmployee(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult getchartByDepartment(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.GetchartByDepartment(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetchartByParticipantDept(string SurveyID)
		{
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.GetchartByParticipantDept(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult getchartByShift(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.GetchartByShift(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetchartByParticipantShift(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.GetchartByParticipantShift(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getchartByDepartmentisStacked(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.getchartByDepartmentisStacked(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult Getlabel(string SurveyID)
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();
            string param = enc.EncryptData(SurveyID + "||");
            try
            {
                response = db.Getlabelanswer(param).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult CheckAnswer(string SurveyID)
        //{
        //    SurveyAndPollsDB db = new SurveyAndPollsDB();
        //    List<clsResponse> response = new List<clsResponse>();
        //    Encryption enc = new Encryption();
        //    string param = enc.EncryptData(SurveyID + "||");
        //    try
        //    {
        //        response = db.CheckAnswer(param).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        response[0].ID = 0;
        //        response[0].Message = ex.Message;
        //        response[0].Contents = "";
        //    }
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult SaveBase64ToImg(List<Base64ToImage> obj) 
        {
            SurveyAndPollsDB db = new SurveyAndPollsDB();
            List<clsResponse> response = new List<clsResponse>();
            Encryption enc = new Encryption();

            try
            {
                response = db.Base64ToImg(obj).ToList();
            }
            catch (Exception ex)
            {
                response[0].ID = 0;
                response[0].Message = ex.Message;
                response[0].Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //public string CreateImage(string data)
        //{
        //    string fname = Server.MapPath("data-image") + "//chart.png";
        //    var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        //    var binData = Convert.FromBase64String(base64Data);
        //    using (var stream = new MemoryStream(binData))
        //    {
        //        System.Drawing.Bitmap img = new Bitmap(stream);
        //        img.Save(fname, System.Drawing.Imaging.ImageFormat.Png);
        //    }
        //    return fname;
        //}

        public ActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Sample data (you can replace this with actual data from your database or model)
            //SurveyAndPollsDB db = new SurveyAndPollsDB();
            //Encryption enc = new Encryption();
            //string param = enc.EncryptData("45" + "||");
            //var data = db.GetchartByShift(param).ToList();

            //// Create a memory stream to save the file in memory
            ////using (var package = new ExcelPackage())
            ////{
            ////    var worksheet = package.Workbook.Worksheets.Add("Data");
            ////    worksheet.Cells["A1"].LoadFromCollection(data, true);

            ////    // Convert the data to an Excel file in memory
            ////    var stream = new MemoryStream(package.GetAsByteArray());

            ////    // Return the Excel file to the user
            ////    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DataExport.xlsx");
            ////}

            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Survey & Polls");

                // Add headers
                worksheet.Cells[2, 1].Value = Session["SurveyTitle"].ToString();
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Value = "By Department";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                var Picture = worksheet.Drawings.AddPicture("byDept", new FileInfo(Path.Combine(Server.MapPath("~/Image/SurveyAndPolls"), "byDept.png"))); 
                Picture.SetPosition(5, 0, 1, 0);
                Picture.SetSize(1000, 800);        // Set image size (100px x 100px)

                var Picture2 = worksheet.Drawings.AddPicture("bydepartment", new FileInfo(Path.Combine(Server.MapPath("~/Image/SurveyAndPolls"), "bydepartment.png")));
                Picture2.SetPosition(49, 0, 1, 0);
                Picture2.SetSize(1000, 800);        // Set image size (100px x 100px)

                worksheet.Cells[93, 1].Value = "By Staff";
                worksheet.Cells[93, 1].Style.Font.Bold = true;

                var Picture3 = worksheet.Drawings.AddPicture("byShift", new FileInfo(Path.Combine(Server.MapPath("~/Image/SurveyAndPolls"), "byShift.png")));
                Picture3.SetPosition(95, 0, 1, 0);
                Picture3.SetSize(1000, 800);        // Set image size (100px x 100px)

                var Picture4 = worksheet.Drawings.AddPicture("Shift", new FileInfo(Path.Combine(Server.MapPath("~/Image/SurveyAndPolls"), "Shift.png")));
                Picture4.SetPosition(139, 0, 1, 0);
                Picture4.SetSize(1000, 800);        // Set image size (100px x 100px)

                // Insert an image into the worksheet from a URL
                //string imageUrl = "https://example.com/path/to/logo.png";  // Replace with the actual image URL
                //var imageBytes = DownloadImage(imageUrl); // Download image to byte array

                //// Insert the image into the worksheet at a specific position
                //var picture = worksheet.Drawings.AddPicture("Logo", imageBytes);
                //picture.SetPosition(1, 0, 5, 0);  // Set position (row 1, col 5)
                //picture.SetSize(100, 100);        // Set image size (100px x 100px)

                // Generate the file as a byte array
                var fileContents = package.GetAsByteArray();

                // Return the file to the client
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ""+ Session["SurveyTitle"].ToString() + ".xlsx");
            }
        }
        private byte[] DownloadImage(string imageUrl)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadData(imageUrl);
            }
        }
        [HttpPost]
        public ActionResult ExportToExcel2(object data)
        {
            // Generate the content to be exported (could be dynamic data or static content)
            //var content = RenderViewToString("Result");

            //// Generate the Excel file from the content
            //using (var package = new ExcelPackage())
            //{
            //    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            //    // Assuming content is HTML-based (can be replaced with any structured data)
            //    var row = 1;
            //    var col = 1;

            //    // Split the content into rows and columns (simple implementation)
            //    var lines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var line in lines)
            //    {
            //        worksheet.Cells[row, col].Value = line;
            //        row++;
            //    }

            //    // Convert the Excel package to a byte array
            //    var stream = new MemoryStream(package.GetAsByteArray());

            //    // Return the Excel file as a downloadable file
            //    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportedContent.xlsx");
            //}

            // Parse the incoming JSON data
            var jsonData = Request.InputStream;
            var reader = new StreamReader(jsonData);
            var content = reader.ReadToEnd();
            var chartData = JsonConvert.DeserializeObject<dynamic>(content);

            // Get the base64-encoded chart image data from the client
            string imageBase64 = chartData.chartImage;

            // Decode the base64 image data
            byte[] imageBytes = Convert.FromBase64String(imageBase64.Substring(imageBase64.IndexOf(",") + 1));

            // Save the image to the server (in a specific folder like App_Data)
            string imagePath = Path.Combine(Server.MapPath("~/App_Data"), "chart_image.png");
            System.IO.File.WriteAllBytes(imagePath, imageBytes);

            // Create an Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Chart Data");

                // Insert the image into the Excel worksheet
                var image = worksheet.Drawings.AddPicture("ChartImage", imagePath);
                image.SetPosition(1, 0, 1, 0);  // Set the position of the image in the worksheet

                // Prepare the Excel file to send to the user
                var fileBytes = package.GetAsByteArray();

                // Return the Excel file for download
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ChartDataWithImage.xlsx");
            }
        }

        [HttpPost]
        public ActionResult SaveImage(ImageData model)
        {
            // Decode the Base64 string
            string[] ModelNew = model.Image.ToString().Split(new[] { "||" }, StringSplitOptions.None);
            model.Image = ModelNew[1];
            string base64String = model.Image.Replace("data:image/png;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Define the path where you want to save the image
            string filePath = Path.Combine(Server.MapPath("~/Image/SurveyAndPolls"), ModelNew[0] + ".png");

            // Save the image to the server
            System.IO.File.WriteAllBytes(filePath, imageBytes);

            return Json(new { success = true });
        }

        public class ImageData
        {
            public string Image { get; set; }
        }

    }
}