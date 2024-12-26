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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using System.Drawing;

namespace PGMEATS_WEB.Controllers
{
    public class MyKYController : Controller
    {
        // GET: MyKY\
        string userID = "";
        public ActionResult Index()
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            userID = Session["LogUserID"].ToString();
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
            dataFrom.CreateUser = userID;

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
        public JsonResult ReplyMyKYSel(String MyKYID)
        {
            clsMyKYDB db = new clsMyKYDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.ReplyMyKYSel(MyKYID);
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
                if (dataFrom.EvidenceAfter != "null")
                {
                    clsConPathFolderDB dbPath = new clsConPathFolderDB();
                    string PathWeb = Server.MapPath("~/Image/MyKY/Response/");


                    string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = dataFrom.EvidenceAfter.Trim();
                    //string fileName = data.SportMasterID.Trim();


                    byte[] bytes = Convert.FromBase64String(dataFrom.FileNameEvidenceAfter);
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



                    //update filename to db
                    var FileName = fileName;
                    dataFrom.EvidenceAfter = FileName + ".PNG";
                }
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
            clsMyKY responseEvidenceAfter = new clsMyKY();
            try
            {
                string filename = "IMG" + MyKYID.Trim();
                responseEvidenceAfter = db.ReplyMyKYSelEvidenceAfter(MyKYID);
                //Evidence After
                string PathWebAfter = Server.MapPath("~/Image/MyKY/Response/");

                
                string fileNameEvidenceAfter = responseEvidenceAfter.EvidenceAfter;
 
                    if (!Directory.Exists(PathWebAfter)) { Directory.CreateDirectory(PathWebAfter); } //jika path web tidak ditemukan makan buat path
                    var ImgWebEvidenceAfter = System.IO.Directory.GetFiles(PathWebAfter + @"\", "*" + fileNameEvidenceAfter ); //get file Web

                    //jika filenya ada maka hapus file web
                    if (ImgWebEvidenceAfter.Length > 0)
                    {
                        for (int i = 0; i <= ImgWebEvidenceAfter.Length - 1; i++)
                        {
                            var LastImgPathEvidenceAfter = ImgWebEvidenceAfter[i];
                            var LastImgNameEvidenceAfter = System.IO.Path.GetFileName(LastImgPathEvidenceAfter);
                            var LastImgFullPathEvidenceAfter = PathWebAfter + LastImgNameEvidenceAfter;
                            System.IO.File.Delete(LastImgFullPathEvidenceAfter);
                        }
                    }
  
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
        public ActionResult DownloadExcel()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "List Of MyKY_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmm") + ".xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }
        public JsonResult ExportExcelMyKYList(clsMyKY dataFrom)
        {
             
            clsMyKYDB db = new clsMyKYDB();
            clsResponse response = new clsResponse();

            var MyKYList = db.ReplyMyKYListExcel(dataFrom);
            var memoryStream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Error Messages");


                // Membuat Header Tabel
                worksheet.Cells["A2"].Value = "PGM EATS - List Of MyKY ";
                worksheet.Cells["A2"].Style.Font.Size = 18;
                worksheet.Cells["A2"].Style.Font.Bold = true;
                worksheet.Cells["A2:D2"].Merge = true;


                worksheet.Cells["A4"].Value = "Periode";
                worksheet.Cells["A4"].Style.Font.Size = 11;
                worksheet.Cells["A4"].Style.Font.Bold = true;
                worksheet.Cells["A4:B4"].Merge = true;

                worksheet.Cells["C4"].Value = ": " + dataFrom.Periode;
                worksheet.Cells["C4"].Style.Font.Size = 11;
                worksheet.Cells["C4"].Style.Font.Bold = true;
                worksheet.Cells["C4:D4"].Merge = true;


                worksheet.Cells["A5"].Value = "Location";
                worksheet.Cells["A5"].Style.Font.Size = 11;
                worksheet.Cells["A5"].Style.Font.Bold = true;
                worksheet.Cells["A5:B5"].Merge = true;

                worksheet.Cells["C5"].Value = ": " + dataFrom.Location;
                worksheet.Cells["C5"].Style.Font.Size = 11;
                worksheet.Cells["C5"].Style.Font.Bold = true;
                worksheet.Cells["C5:D5"].Merge = true;

                worksheet.Cells["A6"].Value = "Status";
                worksheet.Cells["A6"].Style.Font.Size = 11;
                worksheet.Cells["A6"].Style.Font.Bold = true;
                worksheet.Cells["A6:B6"].Merge = true;

                worksheet.Cells["C6"].Value = ":" + dataFrom.Status;
                worksheet.Cells["C6"].Style.Font.Size = 11;
                worksheet.Cells["C6"].Style.Font.Bold = true;
                worksheet.Cells["C6:D6"].Merge = true;

                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                //header
                worksheet.Cells["A9"].Value = "KYID";
                worksheet.Cells["A9"].Style.Font.Size = 11;
                worksheet.Cells["A9"].Style.Font.Bold = true;
                worksheet.Cells["A9:A10"].Merge = true;
                worksheet.Cells["A9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["A9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["A10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["A9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A9"].Style.Fill.BackgroundColor.SetColor(colFromHex);


                worksheet.Cells["B9"].Value = "Date";
                worksheet.Cells["B9"].Style.Font.Size = 11;
                worksheet.Cells["B9"].Style.Font.Bold = true;
                worksheet.Cells["B9:B10"].Merge = true;
                worksheet.Cells["B9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["B9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["B10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["B10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["B9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["B9"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                worksheet.Cells["C9"].Value = "Location";
                worksheet.Cells["C9"].Style.Font.Size = 11;
                worksheet.Cells["C9"].Style.Font.Bold = true;
                worksheet.Cells["C9:C10"].Merge = true;
                worksheet.Cells["C9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["C9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["C10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["C10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["C9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["C9"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                worksheet.Cells["D9"].Value = "Specific Location";
                worksheet.Cells["D9"].Style.Font.Size = 11;
                worksheet.Cells["D9"].Style.Font.Bold = true;
                worksheet.Cells["D9:D10"].Merge = true;
                worksheet.Cells["D9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["D9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["D10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["D10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["D9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["D9"].Style.Fill.BackgroundColor.SetColor(colFromHex);


                worksheet.Cells["E9"].Value = "Description";
                worksheet.Cells["E9"].Style.Font.Size = 11;
                worksheet.Cells["E9"].Style.Font.Bold = true;
                worksheet.Cells["E9:E10"].Merge = true;
                worksheet.Cells["E9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["E9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["E9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["E9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["E9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["E10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["E10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["E10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["E9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["E9"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                worksheet.Cells["F9"].Value = "EVIDENCE";
                worksheet.Cells["F9"].Style.Font.Size = 11;
                worksheet.Cells["F9"].Style.Font.Bold = true;
                worksheet.Cells["F9:G9"].Merge = true;
                worksheet.Cells["F9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["F9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F9"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["F9"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                worksheet.Cells["G9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["G9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["F10"].Value = "PICTURE BEFORE";
                worksheet.Cells["F10"].Style.Font.Size = 11;
                worksheet.Cells["F10"].Style.Font.Bold = true;
                worksheet.Cells["F10"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["F10"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["F10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["F10"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                worksheet.Cells["G10"].Value = "PICTURE AFTER";
                worksheet.Cells["G10"].Style.Font.Size = 11;
                worksheet.Cells["G10"].Style.Font.Bold = true;
                worksheet.Cells["G10"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["G10"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["G10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["G10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["G10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["G10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["G10"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                worksheet.Cells["H9"].Value = "Created By";
                worksheet.Cells["H9"].Style.Font.Size = 11;
                worksheet.Cells["H9"].Style.Font.Bold = true;
                worksheet.Cells["H9:H10"].Merge = true;
                worksheet.Cells["H9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["H9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["H9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["H9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["H9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["H10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["H10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["H10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["H9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["H9"].Style.Fill.BackgroundColor.SetColor(colFromHex);
               
                worksheet.Cells["I9"].Value = "Status";
                worksheet.Cells["I9"].Style.Font.Size = 11;
                worksheet.Cells["I9"].Style.Font.Bold = true;
                worksheet.Cells["I9:I10"].Merge = true;
                worksheet.Cells["I9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["I9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["I9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["I10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["I9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["I9"].Style.Fill.BackgroundColor.SetColor(colFromHex);


                worksheet.Cells["J9"].Value = " Response";
                worksheet.Cells["J9"].Style.Font.Size = 11;
                worksheet.Cells["J9"].Style.Font.Bold = true;
                worksheet.Cells["J9:J10"].Merge = true;
                worksheet.Cells["J9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["J9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["J9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["J9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["J9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["J10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["J10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["J10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["J9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["J9"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                worksheet.Cells["K9"].Value = "Create Response";
                worksheet.Cells["K9"].Style.Font.Size = 11;
                worksheet.Cells["K9"].Style.Font.Bold = true;
                worksheet.Cells["K9:K10"].Merge = true;
                worksheet.Cells["K9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["K9"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["K9"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["K9"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["K9"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["K10"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["K10"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["K10"].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["K9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["K9"].Style.Fill.BackgroundColor.SetColor(colFromHex);

                string imageFolder = Server.MapPath("~/Image/MyKY");
                string imageFolderEvidenceAfter = Server.MapPath("~/Image/MyKY/Response/");

                for (var i = 1; i <= MyKYList.Count; i++)
                {
                    worksheet.Cells[10 + i, 1].Value = MyKYList[i-1].KYID_Eats;
                    worksheet.Cells[10 + i, 1].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[10 + i, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 2].Value = MyKYList[i - 1].CreateDate;
                    worksheet.Cells[10 + i, 2].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[10 + i, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 3].Value = MyKYList[i - 1].MyKYLocationDesc;
                    worksheet.Cells[10 + i, 3].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[10 + i, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 4].Value = MyKYList[i - 1].MyKYSpecLocation;
                    worksheet.Cells[10 + i, 4].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[10 + i, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 5].Value = MyKYList[i - 1].MyKYDesc;
                    worksheet.Cells[10 + i, 5].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[10 + i, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    string imageName = MyKYList[i - 1].Evidence.ToString();
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        // Menambahkan gambar
                    string imagePath = Path.Combine(imageFolder, imageName); // Path ke gambar

                        // Membuka gambar
                        Image imageMyKY = Image.FromFile(imagePath);
                   
                        using (var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            // Tambahkan gambar ke worksheet
                            var picture = worksheet.Drawings.AddPicture($"Image_{i}", imageStream);
                            double columnWidth = worksheet.Column(6).Width;
                            picture.SetPosition(9 + i, 10, 5, 10); // Set posisi gambar (baris dan kolom)
                            picture.SetSize(150, 180); // Responsif dengan kolom

                            // Set tinggi baris agar cocok dengan gambar
                            worksheet.Row(10 + i).Height = 180;
                        }

                    }
                    else
                    {
                        worksheet.Cells[10 + i, 6].Value = "";

                    }

                    string imageNameEvidenceAfter = MyKYList[i - 1].EvidenceAfter.ToString();
                    if (!string.IsNullOrEmpty(imageNameEvidenceAfter))
                    {
                        // Menambahkan gambar
                        string imagePathEvidenceAfter = Path.Combine(imageFolderEvidenceAfter, imageNameEvidenceAfter); // Path ke gambar

                        // Membuka gambar
                        Image imageMyKYEvidenceAfter = Image.FromFile(imagePathEvidenceAfter);

                        using (var imageStreamEvidenceAfter = new FileStream(imagePathEvidenceAfter, FileMode.Open, FileAccess.Read))
                        {
                            // Tambahkan gambar ke worksheet
                            var pictureEvidenceAfter = worksheet.Drawings.AddPicture($"ImageEvidenceAfter_{i}", imageStreamEvidenceAfter);
                            double columnWidthEvidenceAfter = worksheet.Column(7).Width;
                            pictureEvidenceAfter.SetPosition(9 + i, 10, 6, 10); // Set posisi gambar (baris dan kolom)
                            pictureEvidenceAfter.SetSize(150, 180); // Responsif dengan kolom

                            // Set tinggi baris agar cocok dengan gambar
                            worksheet.Row(10 + i).Height = 180;
                        }

                    }
                    else
                    {
                        worksheet.Cells[10 + i, 7].Value = "";

                    }

                    //border
                    worksheet.Cells[10 + i, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 8].Value = MyKYList[i - 1].CreateUser + "\r\n" + MyKYList[i - 1].Section + "\r\n" + MyKYList[i - 1].Shift;
                    worksheet.Cells[10 + i, 8].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[10 + i, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    if (MyKYList[i - 1].Section != "")
                    {
                        worksheet.Cells[10 + i, 8].Style.WrapText = true;
                    }
                    worksheet.Cells[10 + i, 9].Value = MyKYList[i - 1].MyKYStatusDesc;
                    worksheet.Cells[10 + i, 9].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[10 + i, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 10].Value = MyKYList[i - 1].MyKYReply;
                    worksheet.Cells[10 + i, 10].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[10 + i, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[10 + i, 11].Value = MyKYList[i - 1].LastUser;
                    worksheet.Cells[10 + i, 11].Style.Font.Size = 11;
                    worksheet.Cells[10 + i, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[10 + i, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[10 + i, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    //width Column
                    worksheet.Column(1).Width = 20;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(4).Width = 25;
                    worksheet.Column(5).Width = 60;
                    worksheet.Column(6).Width = 24;
                    worksheet.Column(7).Width = 24;
                    worksheet.Column(8).Width = 35;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(10).Width = 60;
                    worksheet.Column(11).Width = 30;

                    //alignment
                    worksheet.Cells[10 + i, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[10 + i, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                }

                //// Menambahkan gambar
                //string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/Images/ovenMangan.png"); // Path ke gambar

                //// Membuka gambar
                //Image image = Image.FromFile(imagePath);

                //// Menambahkan gambar ke worksheet
                //var picture = worksheet.Drawings.AddPicture("MyImage", image);

                //// Mengatur posisi gambar
                //picture.SetPosition(5, 0, 5, 0); // Row 1, Column 1
                //picture.SetSize(300, 100); // Ukuran gambar

                Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
            }
            return Json(new { Status = 200, Message = "" }, JsonRequestBehavior.AllowGet);
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