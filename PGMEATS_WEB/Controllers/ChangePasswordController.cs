using Newtonsoft.Json;
using PGMEATS_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace PGMEATS_WEB.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        public ActionResult Index()
        {
            try
            {
                /*CHECK SESSION LOGIN*/
                if (Session["LogUserID"] is null)
                {
                    return RedirectToAction("Login", "Home");
                }

                string userID = Session["LogUserID"].ToString();
                ViewBag.UserID = userID;
                ClsChangePassword db = new ClsChangePassword();
                ClsChangePassword resp = db.GetPassword(userID);
                return View(resp);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ChangePassword(string Password)
        {
            string UserID = Session["LogUserID"].ToString();
            try
            {
                ClsChangePassword db = new ClsChangePassword();
                string resp = db.ChangePassword(UserID, Password);
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}