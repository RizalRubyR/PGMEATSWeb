using Newtonsoft.Json;
using PGMEATS_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace PGMEATS_WEB.Controllers
{
    public class UserPrivilegeController : Controller
    {
        clsUserPrivilegeDB db = new clsUserPrivilegeDB();

        [HttpGet]
        public ActionResult Index(string UserID)
        {
            try
            {
                var model = new ClsUserPrivilegeViewModel();

                List<clsUserPrivilege> usersprivilege = db.UserPrivilegeList(UserID).ToList();
                var json = JsonConvert.SerializeObject(usersprivilege);
                List<clsUserPrivilege> privileges = JsonConvert.DeserializeObject<List<clsUserPrivilege>>(json);

                foreach (clsUserPrivilege pri in privileges)
                {
                    model.Privileges.Add(pri);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post(ClsUserPrivilegeViewModel model)
        {
            try
            {
                string UserLogin = Session["LogUserID"].ToString();
                int i = 0;
                string msgErr = "";

                foreach (clsUserPrivilege pri in model.Privileges)
                {

                    i = i + db.Update(pri, UserLogin);

                }

                if (i != 0)
                {
                    TempData["Msg"] = "Updated privilege successfully";
                }
                else
                {
                    TempData["Msg"] = msgErr;
                }

                return RedirectToAction("Index", "UserPrivilege", new { UserID = model.Privileges[0].UserID });
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}