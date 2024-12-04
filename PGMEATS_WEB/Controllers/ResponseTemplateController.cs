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
    public class ResponseTemplateController : Controller
    {
        // GET: ResponseTemplate
        public ActionResult Index()
        {
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "A-03";

            clsUserPrivilegeDB db = new clsUserPrivilegeDB();
            clsUserPrivilege data = new clsUserPrivilege();
            data.UserID = userID;
            data.MenuID = MenuID;
            data.AdminStatus = AdminStatus;

            clsUserPrivilege Privilege = db.UserPrivilegeCheck("3", data);
            if (Privilege.AllowAccess == "0")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.AllowUpdate = Privilege.AllowUpdate;
            ViewBag.UserID = userID;

            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult GetList()
        {
            List<clsResponseTemplate> data = new List<clsResponseTemplate>();
            clsResponseTemplateDB db = new clsResponseTemplateDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult Insert(clsResponseTemplate data)

        {
            data.UpdateUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsResponseTemplateDB DB = new clsResponseTemplateDB();
            try
            {
                response = DB.Insert(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult Update(clsResponseTemplate data)

        {
            data.UpdateDate = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsResponseTemplateDB DB = new clsResponseTemplateDB();
            try
            {
                response = DB.Update(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult Delete(int ResponseID)

        {
            clsResponse response = new clsResponse();
            clsResponseTemplateDB DB = new clsResponseTemplateDB();
            try
            {
                response = DB.Delete(ResponseID);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}