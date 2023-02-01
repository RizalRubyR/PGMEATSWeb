using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PGMEATS_WEB.Models;

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
        public JsonResult IssueTypeList(String IssueTypeID)
        {
            List<clsIssueTypeMaster> data = new List<clsIssueTypeMaster>();
            clsIssueTypeMaterDB db = new clsIssueTypeMaterDB();
            clsResponse response = new clsResponse();
            try
            {
                response = db.IssueTypeList(IssueTypeID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs("GET", "POST")]
        //[HttpPost]
        //public JsonResult IssueTypeDetail(String IssueTypeID)
        //{
        //    List<clsIssueTypeMaster> data = new List<clsIssueTypeMaster>();
        //    clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
        //    try
        //    {
        //        data = DB.IssueTypeList(IssueTypeID);
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}


        //[AcceptVerbs("GET", "POST")]
        //[HttpPost]
        //public JsonResult IssueTypeIns(String IssueTypeID, string IssueTypeDesc, string ActiveStatus)
        //{
        //    List<clsResponse> response = new List<clsResponse>();
        //    clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
        //    try
        //    {
        //        var user = Session["user"].ToString();
        //        var i = DB.IssueTypeUpd(IssueTypeID, IssueTypeDesc, ActiveStatus, user);
        //        if ()
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Json(i, JsonRequestBehavior.AllowGet);
        //}


        //[AcceptVerbs("GET", "POST")]
        //[HttpPost]
        //public JsonResult IssueTypeDel(String IssueTypeID)
        //{
        //    List<clsIssueTypeMaster> data = new List<clsIssueTypeMaster>();
        //    clsIssueTypeMaterDB DB = new clsIssueTypeMaterDB();
        //    try
        //    {
        //        data = DB.IssueTypeList(IssueTypeID).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

    }
}