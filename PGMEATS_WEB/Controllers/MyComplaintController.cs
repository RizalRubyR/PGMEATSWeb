using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public JsonResult IssueTypeList()
        {

        }

    }
}