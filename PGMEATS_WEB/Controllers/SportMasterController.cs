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
    public class SportMasterController : Controller
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
            string MenuID = "H-01";

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
        public JsonResult SportMasterList()
        {
            List<clsSportMaster> data = new List<clsSportMaster>();
            clsSportMasterDB db = new clsSportMasterDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.SportMasterList();
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
            List<clsSportMaster> data = new List<clsSportMaster>();
            clsSportMasterDB db = new clsSportMasterDB();
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
        public JsonResult SportMasterIns(clsSportMaster data)

        {
            data.LastUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsSportMasterDB DB = new clsSportMasterDB();
            try
            {
                response = DB.SportMasterIns(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult SportMasterUpd(clsSportMaster data)

        {
            data.LastUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsSportMasterDB DB = new clsSportMasterDB();
            try
            {
                response = DB.SportMasterUpd(data, "1");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult SportMasterDel(String SportID)
        {
            clsResponse response = new clsResponse();
            clsSportMasterDB DB = new clsSportMasterDB();
            try
            {
                string filename = "IMG" + SportID.Trim();
                response = DB.SportMasterDel(SportID);

                if (response.Message.ToLower().Contains("success"))
                {
                    clsConPathFolderDB dbPath = new clsConPathFolderDB();
                    string PathWeb = dbPath.PathFolder("Web", "SportMaster"); //Jika Web
                    string PathMobile = dbPath.PathFolder("Mobile", "SportMaster"); //Jika Mobile

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
        public JsonResult ImgSave(clsSportMaster data)
        {
            clsResponse response = new clsResponse();
            clsSportMasterDB DB = new clsSportMasterDB();
            try
            {
                clsConPathFolderDB dbPath = new clsConPathFolderDB();
                string PathWeb = dbPath.PathFolder("Web", "SportMaster"); //Jika Web
                string PathMobile = dbPath.PathFolder("Mobile", "SportMaster"); //Jika Mobile 

                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = "IMG" + data.SportID.Trim();
                //string fileName = data.SportMasterID.Trim();


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

                    var ImgPathWeb = PathWeb + fileName; //file name web
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

                    var ImgPathMobile = PathMobile + fileName; //file name mobile
                    img.Save(ImgPathMobile, "PNG"); //save to mobile

                }

                //update filename to db
                var FileName = fileName + ".PNG";
                data.LastUser = Session["LogUserID"].ToString();
                data.FileName = FileName;
                response = DB.SportMasterUpd(data, "1");
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ImgSaveAfterClick(clsSportMaster data)
        {
            clsResponse response = new clsResponse();
            clsSportMasterDB DB = new clsSportMasterDB();
            try
            {
                clsConPathFolderDB dbPath = new clsConPathFolderDB();
                string PathWeb = dbPath.PathFolder("Web", "SportMaster"); //Jika Web
                string PathMobile = dbPath.PathFolder("Mobile", "SportMaster"); //Jika Mobile 

                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileName = "IMG" + data.SportID.Trim() + "_Clicked";
                //string fileName = data.SportMasterID.Trim();


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

                    var ImgPathWeb = PathWeb + fileName; //file name web
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

                    var ImgPathMobile = PathMobile + fileName; //file name mobile
                    img.Save(ImgPathMobile, "PNG"); //save to mobile

                }

                //update filename to db
                var FileName = fileName + ".PNG";
                data.LastUser = Session["LogUserID"].ToString();
                data.FileName2 = FileName;
                response = DB.SportMasterUpd(data, "2");
            }

            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFillCombo(String TypeAction, String Param)
        {
            Response resp = new Response();
            ComboFilter db = new ComboFilter();
            try
            {
                resp = db.FillCombo(TypeAction, Param);
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.ID = "1";
                resp.Content = "";

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
        }

    }
}