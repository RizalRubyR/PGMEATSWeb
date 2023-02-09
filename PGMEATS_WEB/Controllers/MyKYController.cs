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
    public class MyKYController : Controller
    {
        // GET: MyKY
        public ActionResult Index()
        {
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ReplyMyKYList(clsMyKY dataFrom)
        {
            clsMyKYDB db = new clsMyKYDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.ReplyMyKYList(dataFrom);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ReplyMyKYUpd(clsMyKY dataFrom)
        {
            clsMyKYDB db = new clsMyKYDB();
            clsResponse response = new clsResponse();
            try
            {
                dataFrom.CreateUser = Session["LogUserID"].ToString();
                response = db.ReplyMyKYUpd(dataFrom);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ReplyMyKYDel(string MyKYID)
        {
            clsMyKYDB db = new clsMyKYDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.ReplyMyKYDel(MyKYID);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult FillCombo(String Type)
        {
            List<clsMyKY> data = new List<clsMyKY>();
            clsMyKYDB db = new clsMyKYDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.FillCombo(Type);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}