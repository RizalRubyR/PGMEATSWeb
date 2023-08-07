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
    public class NewsListController : Controller
    {
        // GET: NewsList
        public ActionResult Index()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string userID = Session["LogUserID"].ToString();
            string AdminStatus = Session["AdminStatus"].ToString();
            string UserType = Session["UserType"].ToString();
            string MenuID = "B-01";

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
            ViewBag.AdminStatus = AdminStatus;
            ViewBag.UserType = UserType;

            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult GetNewsList(string datefrom, string dateTo, string groupdepartment, string designation)
        {            
            clsNewsDB db = new clsNewsDB();
            clsResponse response = new clsResponse();
            try
            {
                var user = Session["LogUserID"].ToString();
                response = db.NewsList(user, datefrom, dateTo, groupdepartment, designation);
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
        public JsonResult FillDetail(string NewsID)
        {
            clsNewsDB db = new clsNewsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetDataDetail(NewsID);
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
        public JsonResult FillDetailPopUp(string NewsID)
        {
            clsNewsDB db = new clsNewsDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.GetDataDetailPopUp(NewsID);
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
            clsNewsDB db = new clsNewsDB();
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
        public JsonResult DownloadFile(string FileName)
        {
            clsResponse response = new clsResponse();
            try
            {
                clsConPathFolderDB dbPath = new clsConPathFolderDB();
                string path = dbPath.PathFolder("Web", "News"); //Jika Web

                //Read the File as Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path + FileName);

                //Convert File to Base64 string and send to Client.
                string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

                response.ID = 1;
                response.Message = "Success";
                response.Contents = "";//base64;
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message.ToString();
                response.Message = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult SendData(clsNews data)
        {
            data.User = Session["LogUserID"].ToString().Trim();
            clsResponse response = new clsResponse();
            clsNewsDB DB = new clsNewsDB();
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
        public JsonResult DeleteData(clsNews data)
        {            
            clsResponse response = new clsResponse();
            clsNewsDB DB = new clsNewsDB();
            try
            {
                response = DB.Delete(data);
                if (response.ID != 0)
                {
                    DeleteFile(data);
                }
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
        public JsonResult DeleteAttachment(clsNews data)
        {
            clsResponse response = new clsResponse();
            clsNewsDB DB = new clsNewsDB();
            try
            {
                response = DB.DeleteAttachment(data);
                if (response.ID != 0)
                {
                    DeleteFile(data);
                }
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
        public JsonResult UploadFile(string NewsID, string type, string oldAttachment)
        {
            clsConPathFolderDB dbPath = new clsConPathFolderDB();
            string PathWeb = dbPath.PathFolder("Web", "News"); //Jika Web
            string PathMobile = dbPath.PathFolder("Mobile", "News"); //Jika Mobile 
            string seqFileName = Convert.ToInt32(NewsID.Trim()).ToString("00000");
            string FileName = "";

            clsResponse response = new clsResponse();
            clsNews data = new clsNews();
            clsNewsDB DB = new clsNewsDB();
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        FileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + seqFileName + Path.GetExtension(file.FileName);

                        if (PathWeb != "")
                        {
                            //jika path web tidak ditemukan maka buat path
                            if (!Directory.Exists(PathWeb))
                            {
                                Directory.CreateDirectory(PathWeb);
                            }

                            var relativePath = PathWeb + FileName;
                            //var absolutePath = HttpContext.Server.MapPath(PathWeb);

                            if (System.IO.File.Exists(relativePath))
                            {
                                System.IO.File.Delete(relativePath);
                            }

                            file.SaveAs(relativePath);
                        }

                        if (PathMobile != "")
                        {
                            //jika path tidak ditemukan maka buat path
                            if (!Directory.Exists(PathMobile))
                            {
                                Directory.CreateDirectory(PathMobile);
                            }

                            var relativePath = PathMobile + FileName;
                            //var absolutePath = HttpContext.Server.MapPath(relativePath);

                            if (System.IO.File.Exists(relativePath))
                            {
                                System.IO.File.Delete(relativePath);
                            }

                            file.SaveAs(relativePath);
                        }

                        data.NewsID = NewsID;
                        data.Attachment = FileName;
                        response = DB.UpdateFile(data);

                        if(type == "1")
                        {
                            data.Attachment = oldAttachment;
                            DeleteFile(data);
                        }
                    }
                }
                else
                {
                    response.ID = 1;
                    response.Message = "";
                    response.Contents = "";
                }
            }
            catch (Exception ex)
            {
                response.ID = 0;
                response.Message = ex.Message;
                response.Contents = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private void DeleteFile(clsNews data)
        {
            try
            {
                clsConPathFolderDB dbPath = new clsConPathFolderDB();
                clsNewsDB DB = new clsNewsDB();
                string PathWeb = dbPath.PathFolder("Web", "News"); //Jika Web
                string PathMobile = dbPath.PathFolder("Mobile", "News"); //Jika Mobile 

                string FileName = data.Attachment;

                if (PathWeb != "")
                {
                    if (Directory.Exists(PathWeb))
                    {
                        var relativePath = PathWeb + FileName;

                        if (System.IO.File.Exists(relativePath))
                        {
                            System.IO.File.Delete(relativePath);
                        }
                    }
                }

                if (PathMobile != "")
                {
                    if (Directory.Exists(PathMobile))
                    {
                        var relativePath = PathMobile + FileName;

                        if (System.IO.File.Exists(relativePath))
                        {
                            System.IO.File.Delete(relativePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}