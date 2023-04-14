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
    public class UserStaffController : Controller
    {
        // GET: UserSetup
        clsUserStaffDB db = new clsUserStaffDB();
        public ActionResult Index()
        {
            try
            {
                /*CHECK SESSION LOGIN*/
                if (Session["LogUserID"] is null)
                {
                    return RedirectToAction("Login", "Home");
                }

                string userID = Session["LogUserID"] + "";
                string MenuID = "G-02 ";

                clsUserPrivilegeDB db = new clsUserPrivilegeDB();
                clsUserPrivilege data = new clsUserPrivilege();
                data.UserID = userID;
                data.MenuID = MenuID;
             
                /*CHECK PRIVILEGE*/
                clsUserPrivilege Privilege = db.UserPrivilegeCheck("3", data);
                if(Privilege.AllowAccess == "0")
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.AllowUpdate = Privilege.AllowUpdate;
                ViewBag.UserID = userID;

                return View();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        public JsonResult GetList()
        {
            try
            {
                List<clsUserStaff> users = db.UserList().ToList();

                var jsonResult = Json(users, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
        public JsonResult GetListByStaffID(string StaffID)
        {
            try
            {
                List<clsUserStaff> users = db.UserListByUserID(StaffID).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

        }
        public JsonResult Disabled(clsUserStaff user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                db.Disabled(user.StaffID);
                bSuccess = true;
                msg = "Disabled Data Success";
                ModelState.Clear();

                return new JsonResult
                {
                    Data = new { ErrorMessage = msg, Success = bSuccess },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
        public JsonResult Enabled(clsUserStaff user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                db.Enabled(user.StaffID);
                bSuccess = true;
                msg = "Enabled Data Success";
                ModelState.Clear();

                return new JsonResult
                {
                    Data = new { ErrorMessage = msg, Success = bSuccess },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
        public JsonResult UpdateData(clsUserStaff user)
        {
            string UserLogin = Session["LogUserID"].ToString();
            bool bSuccess;
            string msg = "";

            try
            {
                db.Update(user, UserLogin);
                bSuccess = true;
                ModelState.Clear();

                return new JsonResult
                {
                    Data = new { ErrorMessage = msg, Success = bSuccess },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }
        public JsonResult ResetPassword(clsUserStaff user)
        {
            string UserLogin = Session["LogUserID"].ToString();
            bool bSuccess;
            string msg = "";

            try
            {
                db.ResetPassword(user, UserLogin);
                bSuccess = true;
                ModelState.Clear();

                return new JsonResult
                {
                    Data = new { ErrorMessage = msg, Success = bSuccess },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
        }

    }
}