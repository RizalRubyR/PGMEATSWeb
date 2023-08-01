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
    public class BirthdayNotificationTemplateController : Controller
    {
        public ActionResult Index()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "F-01 ";

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

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult GetBirthdayTemplateList()
        {            
            clsBirthdayNotificationTemplateDB db = new clsBirthdayNotificationTemplateDB();
            clsResponse response = new clsResponse();
            try
            {
                var user = Session["LogUserID"].ToString();
                response = db.GetBirthdayNotificationTemplateList();
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
        public JsonResult GetBirthdayTemplateListByID(string BirthdayTemplateID)
        {
            clsBirthdayNotificationTemplateDB db = new clsBirthdayNotificationTemplateDB();
            clsResponse response = new clsResponse();
            try
            {
                var user = Session["LogUserID"].ToString();
                response = db.GetBirthdayNotificationTemplateListByBirthdayTemplateID(BirthdayTemplateID);
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
        public JsonResult InsertData(clsBirthdayNotificationTemplate data)
        {
            data.CreateUser = Session["LogUserID"].ToString().Trim();
            clsResponse response = new clsResponse();
            clsBirthdayNotificationTemplateDB DB = new clsBirthdayNotificationTemplateDB();
            try
            {
                response = DB.InsertUpdate(data);
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
        public JsonResult DeleteData(clsBirthdayNotificationTemplate data)
        {            
            clsResponse response = new clsResponse();
            clsBirthdayNotificationTemplateDB DB = new clsBirthdayNotificationTemplateDB();
            try
            {
                response = DB.Delete(data);
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