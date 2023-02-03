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
            return View();
        }

        public ActionResult CanteenComplaintList()
        {
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
                response = DB.IssueTypeDel(IssueTypeID);

                if (response.Message.ToLower().Contains("success"))
                {
                    string PathFolder = Server.MapPath("~/Image/IssueTypeMaster/");
                    string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (!Directory.Exists(PathFolder)) { Directory.CreateDirectory(PathFolder); } //jika pathfolder tidak ditemukan makan buat path
                    var lastImg = System.IO.Directory.GetFiles(PathFolder + @"\", "*" + IssueTypeID.Trim() + "*.PNG"); //get file

                    if (lastImg.Length > 0)
                    {
                        for (int i = 0; i <= lastImg.Length - 1; i++)
                        {
                            var LastImgPath = lastImg[i];
                            var LastImgName = System.IO.Path.GetFileName(LastImgPath);
                            var LastImgFullPath = PathFolder + LastImgName;
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
                string PathFolder = Server.MapPath("~/Image/IssueTypeMaster/");
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (!Directory.Exists(PathFolder)) { Directory.CreateDirectory(PathFolder); } //jika pathfolder tidak ditemukan makan buat path
                var lastImg = System.IO.Directory.GetFiles(PathFolder + @"\", "*" + data.IssueTypeID.Trim() + "*.PNG"); //get file

                //jika filenya ada maka hapus file
                if (lastImg.Length > 0)
                {
                    for (int i = 0; i <= lastImg.Length - 1; i++)
                    {
                        var LastImgPath = lastImg[i];
                        var LastImgName = System.IO.Path.GetFileName(LastImgPath);
                        var LastImgFullPath = PathFolder + LastImgName;
                        System.IO.File.Delete(LastImgFullPath);
                    }
                }

                byte[] bytes = Convert.FromBase64String(data.files);

                //simpan image
                WebImage img = new WebImage(bytes);
                if (img.Width > 1000) { img.Resize(1000, 1000); }  //convert image size
                var ImgPath = PathFolder + data.IssueTypeID.Trim() + "_" + date + "_";
                img.Save(ImgPath, "PNG");

                //update filename to db
                var FileName = data.IssueTypeID.Trim() + "_" + date + "_" + ".PNG";
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