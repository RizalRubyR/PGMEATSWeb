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
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "E-01";

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
                string filename = "IMG" + MyKYID.Trim();
                response = db.ReplyMyKYDel(MyKYID);

                if (response.Message.ToLower().Contains("success"))
                {
                    clsConPathFolderDB dbPath = new clsConPathFolderDB();
                    string PathWeb = dbPath.PathFolder("Web", "MyKY"); //Jika Web
                    string PathMobile = dbPath.PathFolder("Mobile", "MyKY"); //Jika Mobile

                    string date = DateTime.Now.ToString("yyyyMMddHHmmss");

                    if (!Directory.Exists(PathWeb)) { Directory.CreateDirectory(PathWeb); } //jika path web tidak ditemukan makan buat path            
                    if (!Directory.Exists(PathMobile)) { Directory.CreateDirectory(PathMobile); } //jika path mobile tidak ditemukan makan buat path

                    var ImgWeb = System.IO.Directory.GetFiles(PathWeb + @"\", "*" + filename + "*.PNG"); //get file Web
                    var ImgMobile = System.IO.Directory.GetFiles(PathMobile + @"\", "*" + filename + "*.PNG"); //get file Mobile

                    //jika filenya ada maka hapus file web
                    if (ImgWeb.Length > 0)
                    {
                        for (int i = 0; i <= ImgWeb.Length - 1; i++)
                        {
                            var LastImgPath = ImgWeb[i];
                            var LastImgName = System.IO.Path.GetFileName(LastImgPath);
                            var LastImgFullPath = PathWeb + LastImgName;
                            System.IO.File.Delete(LastImgFullPath);
                        }
                    }

                    //jika filenya ada maka hapus file mobile
                    if (ImgMobile.Length > 0)
                    {
                        for (int i = 0; i <= ImgMobile.Length - 1; i++)
                        {
                            var LastImgPath = ImgMobile[i];
                            var LastImgName = System.IO.Path.GetFileName(LastImgPath);
                            var LastImgFullPath = PathMobile + LastImgName;
                            System.IO.File.Delete(LastImgFullPath);
                        }
                    }
                }
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