using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using PGMEATS_WEB.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.Web.Routing;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;

namespace PGMEATS_WEB.Controllers
{
    public class HomeController : Controller
    {        
        [SessionExpire]
        [SessionTimeout]
        public ActionResult Index()
        {
            string userID = Session["LogUserID"] + "";
            ViewBag.UserID = userID;

            return View();
        }

        [HttpGet]
        public ActionResult Login(string logout)
        {
            if (logout == "1" || logout != null)
            {
                Session["LogUserID"] = null;
                Session.Clear();
                Session.Abandon();
            }

            if (logout == "1")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        //Commnet,causing issue when login without API
        //[HttpPost]
        public ActionResult Login(clsUser User)
        {
            string message = "";
            Encryption encrypt = new Encryption();
            if (ModelState.IsValid)
            {
                try
                {
                    clsUserDB db = new clsUserDB();
                    string UserID = User.UserID;
                    string Password = User.Password;
                    User = db.GetUser(UserID);
                    if (Password == User.Password)
                    {
                        message = "Success";
                        Session["LogUserID"] = User.UserID.ToString();
                    }
                    else
                    {
                        message = "User ID or Password is incorrect!";
                    }
                  
                }
                catch (Exception ex)
                {
                    message = ex.Message.ToString().Trim();
                }
            }
            else
            {
                if (User.UserID == null)
                {
                    message = "Please input User ID!";
                }
                else if (User.Password == null)
                {
                    message = "Please input Password!";
                }
            }

            ViewBag.Message = message;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Error500(string Error_Message)
        {
            ViewBag.Error_Message = Error_Message;
            return View();
        }
    }
}