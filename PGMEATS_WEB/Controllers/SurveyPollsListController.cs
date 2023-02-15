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
            return View();
        }

        public ActionResult Create()
        {
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
                response = db.GetSurveyAndPolls();
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