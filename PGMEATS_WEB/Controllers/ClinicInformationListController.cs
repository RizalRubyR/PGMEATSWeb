using PGMEATS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace PGMEATS_WEB.Controllers
{
    [SessionExpire]
    [SessionTimeout]
    public class ClinicInformationListController : Controller
    {
        // GET: ClinicInformationList
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string pRegion,string pClinicName,string pDescription,string pURL,string pURLDisplay)
        {
            clsClinicInformationList clsClinic = new clsClinicInformationList();
            clsClinic.Region = pRegion;
            clsClinic.ClinicName = pClinicName;
            clsClinic.Description = pDescription;
            clsClinic.URL = pURL;
            clsClinic.URLDisplay = pURLDisplay;
            clsClinic.UserCreate = Session["LogUserID"].ToString();
            clsClinic.CreateDate = DateTime.Now;
            try
            {
                clsClinicInformationListDB db = new clsClinicInformationListDB();
                db.Insert(clsClinic);

                TempData["Message"] = "Data saved successfully!";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(string pClinicName)
        {
            TempData["CreatePartlist"] = "1";
            ViewBag.Message = "";

            clsClinicInformationListDB db = new clsClinicInformationListDB();
            clsClinicInformationList clsClinic = db.Clinic.Single(usr => usr.ClinicName == pClinicName);
            UpdateModel(clsClinic, null, null, new string[] { "ClinicName" });
            if (ModelState.IsValid)
            {
                try
                {
                    clsClinic.UserUpdate = Session["LogUserId"].ToString();
                    db.Update(clsClinic);

                    TempData["Message"] = "Data updated successfully!";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());

                    return View(clsClinic);
                }
            }
            return View(clsClinic);
        }

        [HttpPost]
        public JsonResult ValidateInsert(string pClinicName)
        {
            bool status = false;
            clsClinicInformationListDB db = new clsClinicInformationListDB();
            clsClinicInformationList model = db.GetData(pClinicName);
            if (model == null)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Post(string pRegion,string pClinicName)
        {
            TempData["CreateCliniclist"] = "1";
            ViewBag.Message = "";
            clsClinicInformationListDB db = new clsClinicInformationListDB();
            db.Delete(pRegion,pClinicName);
            TempData["Message"] = "Data deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}