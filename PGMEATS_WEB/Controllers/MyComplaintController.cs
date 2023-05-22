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
    public class MyComplaintController : Controller
    {
        // GET: MyComplaint
        public ActionResult IssueTypeMaster()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "A-01";

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

        public ActionResult CanteenComplaintList()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "A-02";

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
        public JsonResult IssueTypeList()
        {
            List<clsIssueTypeMaster> data = new List<clsIssueTypeMaster>();
            clsIssueTypeMaterDB db = new clsIssueTypeMaterDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.IssueTypeList();
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
            List<clsIssueTypeMaster> data = new List<clsIssueTypeMaster>();
            clsIssueTypeMaterDB db = new clsIssueTypeMaterDB();
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

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult IssueTypeIns(clsIssueTypeMaster data)

        {
            data.LastUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
            try
            {
                response = DB.IssueTypeIns(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult IssueTypeUpd(clsIssueTypeMaster data)

        {
            data.LastUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
            try
            {
                response = DB.IssueTypeUpd(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult IssueTypeDel(String IssueTypeID)
        {
            clsResponse response = new clsResponse();
            clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
            try
            {
                string filename = "IMG" + IssueTypeID.Trim();
                response = DB.IssueTypeDel(IssueTypeID);

                if (response.Message.ToLower().Contains("success"))
                {
                    clsConPathFolderDB dbPath = new clsConPathFolderDB();
                    string PathWeb = dbPath.PathFolder("Web", "IssueType"); //Jika Web
                    string PathMobile = dbPath.PathFolder("Mobile", "IssueType"); //Jika Mobile

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
        public JsonResult ReplyComplaintList(clsMyComplaint dataFrom)
        {
            clsMyComplaintDB db = new clsMyComplaintDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.ReplyComplaintList(dataFrom);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ReplyComplaintUpd(clsMyComplaint dataFrom)
        {
            clsMyComplaintDB db = new clsMyComplaintDB();
            clsResponse response = new clsResponse();
            try
            {
                dataFrom.CreatedUser = Session["LogUserID"].ToString();
                response = db.ReplyComplaintUpd(dataFrom);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ReplyComplaintDel(string ComplaintID)
        {
            clsMyComplaintDB db = new clsMyComplaintDB();
            clsResponse response = new clsResponse();
            try
            {
                string filename = "IMG" + ComplaintID.Trim();
                response = db.ReplyComplaintDel(ComplaintID);

                if (response.Message.ToLower().Contains("success"))
                {
                    clsConPathFolderDB dbPath = new clsConPathFolderDB();
                    string PathWeb = dbPath.PathFolder("Web", "MyComplaint"); //Jika Web
                    string PathMobile = dbPath.PathFolder("Mobile", "MyComplaint"); //Jika Mobile

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
        public JsonResult ImgSave(clsIssueTypeMaster data)
        {
            clsResponse response = new clsResponse();
            clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
            try
            {
                clsConPathFolderDB dbPath = new clsConPathFolderDB();
                string PathWeb = dbPath.PathFolder("Web", "IssueType"); //Jika Web
                string PathMobile = dbPath.PathFolder("Mobile", "IssueType"); //Jika Mobile 

                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = "IMG" + data.IssueTypeID.Trim();
                //string fileName = data.IssueTypeID.Trim();


                byte[] bytes = Convert.FromBase64String(data.files);
                WebImage img = new WebImage(bytes);
                if (img.Width > 1000) { img.Resize(1000, 1000); }  //convert image size

                if (PathWeb != "")
                {
                    if (!Directory.Exists(PathWeb)) { Directory.CreateDirectory(PathWeb); } //jika path web tidak ditemukan makan buat path
                    var ImgWeb = System.IO.Directory.GetFiles(PathWeb + @"\", "*" + fileName + "*.PNG"); //get file Web

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

                    var ImgPathWeb = PathWeb + fileName + "_" + date + "_"; //file name web
                    img.Save(ImgPathWeb, "PNG"); //save to web
                }

                if (PathMobile != "")
                {
                    if (!Directory.Exists(PathMobile)) { Directory.CreateDirectory(PathMobile); } //jika path mobile tidak ditemukan makan buat path
                    var ImgMobile = System.IO.Directory.GetFiles(PathMobile + @"\", "*" + fileName + "*.PNG"); //get file Mobile

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

                    var ImgPathMobile = PathMobile + fileName + "_" + date + "_"; //file name mobile
                    img.Save(ImgPathMobile, "PNG"); //save to mobile

                }

                //update filename to db
                var FileName = fileName + "_" + date + "_" + ".PNG";
                data.LastUser = Session["LogUserID"].ToString();
                data.FileName = FileName;
                response = DB.IssueTypeUpd(data);
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}