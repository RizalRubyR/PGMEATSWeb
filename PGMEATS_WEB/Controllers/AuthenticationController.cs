using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PGMEATS_WEB.Models;

namespace PGMEATS_WEB.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [JsonAuthorize]
        public JsonResult CheckSessionExpired()
        {
            return Json(Session["LogUserID"].ToString() + "", JsonRequestBehavior.AllowGet);
        }
    }
}