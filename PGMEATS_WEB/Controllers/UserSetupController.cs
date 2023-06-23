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
    public class UserSetupController : Controller
    {
        // GET: UserSetup
        clsUserSetupDB db = new clsUserSetupDB();
        public ActionResult Index()
        {
            try
            {
                /*CHECK SESSION LOGIN*/
                if (Session["LogUserID"] is null)
                {
                    return RedirectToAction("Login", "Home");
                }

                string userID = Session["LogUserID"].ToString();
                string AdminStatus = Session["AdminStatus"].ToString();
                string MenuID = "G-01";

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
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public ActionResult UserPrivilege(string userID)
        {
            /*CHECK SESSION LOGIN*/
            if (Session["LogUserID"] is null)
            {
                return RedirectToAction("Login", "Home");
            }

            string AdminStatus = Session["AdminStatus"].ToString();
            string MenuID = "G-01";

            clsUserPrivilegeDB db = new clsUserPrivilegeDB();
            clsUserPrivilege data = new clsUserPrivilege();
            data.UserID = Session["LogUserID"].ToString();
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

        public JsonResult GetList()
        {
            try
            {
                List<clsUserSetup> users = db.UserList().ToList();

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

        public JsonResult GetListByUserID(string UserID)
        {
            Response resp = new Response();
            try
            {
                resp = db.UserListByUserID(UserID);
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

        public JsonResult InsertData(clsUserSetup user)
        {
            string UserLogin = Session["LogUserID"].ToString();
            string msg = "";

            try
            {
                msg = db.Insert(user, UserLogin);
                return Json(msg, JsonRequestBehavior.AllowGet);

                //msg = ValidasiJSOXInsert(user.UserID, user.Password);
                //if (msg == "")
                //{
                //    db.Insert(user, UserLogin);
                //    db.InsertJSOXHistory(user, UserLogin);
                //    bSuccess = true;
                //    ModelState.Clear();

                //    return new JsonResult
                //    {
                //        Data = new { ErrorMessage = msg, Success = bSuccess },
                //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //    };
                //}
                //else
                //{
                //    bSuccess = false;
                //    ModelState.Clear();

                //    return new JsonResult
                //    {
                //        Data = new { ErrorMessage = msg, Success = bSuccess },
                //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //    };
                //}

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    

    public JsonResult UpdateData(clsUserSetup user)
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

    public JsonResult DeleteData(clsUserSetup user)
    {
        bool bSuccess;
        string msg = "";

        try
        {
            db.Delete(user.UserID);
            bSuccess = true;
            msg = "Delete Data Success";
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

    private string ValidasiJSOXInsert(string pUserID, string pPassword)
    {
        try
        {
            clsJSOXSetup cJSOXSetup = new clsJSOXSetup();
            cJSOXSetup.RuleID = "1";
            clsJSOXSetup jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            int MinimumLength = jsox.ParamValue;
            string sMsg = "";
            if (jsox.Enable && pPassword.Length < MinimumLength)
            {
                sMsg = "Password length minimum " + MinimumLength + " characters";
                return sMsg;
            }

            cJSOXSetup.RuleID = "7";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            if (jsox.Enable == false && pPassword.ToUpper().Contains(pUserID.ToUpper()) == true)
            {
                sMsg = "Password cannot contain User ID";
                return sMsg;
            }

            cJSOXSetup.RuleID = "8";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);

            string pPasswordNumber = string.Empty;

            for (int i = 0; i < pPassword.Length; i++)
            {
                if (Char.IsDigit(pPassword[i]))
                    pPasswordNumber += pPassword[i];
            }

            if (jsox.Enable && pPasswordNumber.Length < jsox.ParamValue)
            {
                sMsg = "Password must contain minimum " + jsox.ParamValue + " numeric";
                return sMsg;
            }

            cJSOXSetup.RuleID = "9";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            if (jsox.Enable == false)
            {
                string PrevChar = pPassword.Substring(0, 1);
                for (int i = 2; i <= pPassword.Length; i++)
                {
                    if (pPassword.Substring(i - 1, 1) == PrevChar)
                    {
                        sMsg = "Password cannot contain repeating characters";
                        return sMsg;
                    }
                    else
                        PrevChar = pPassword.Substring(i - 1, 1);
                }
            }

            bool PasswordExpired = false;
            clsPasswordHistory cPasswordHistory = new clsPasswordHistory();
            cPasswordHistory.UserID = pUserID;
            clsPasswordHistory His = clsPasswordHistoryDB.GetLastData(cPasswordHistory);
            if (His != null)
            {
                cJSOXSetup = new clsJSOXSetup();
                cJSOXSetup.RuleID = "3";
                jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
                DateTime ServerDate = clsPasswordHistoryDB.GetServerDate();

                //int dif = DateDiff(DateInterval.Day, His.UpdateDate, ServerDate);
                int dif = int.Parse((ServerDate - His.UpdateDate).Days.ToString());
                int expireDay = jsox.ParamValue;
                if (dif >= expireDay)
                {
                    cJSOXSetup.RuleID = "2";
                    jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
                    int agingDay = jsox.ParamValue;
                    if (jsox.Enable)
                    {
                        if (dif >= expireDay)
                            PasswordExpired = true;
                    }
                }
            }

            if (PasswordExpired == true)
            {
                cJSOXSetup.RuleID = "5";
                jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
                if (jsox.Enable & jsox.ParamValue > 0)
                {
                    clsPasswordHistoryDB PasswordHistoryDB = new clsPasswordHistoryDB();
                    List<clsPasswordHistory> PassList = PasswordHistoryDB.GetList(pUserID, jsox.ParamValue.ToString().Trim()).ToList();

                    Encryption encrypt = new Encryption();

                    foreach (var Pwd in PassList)
                    {
                        string OldPwd = encrypt.DecryptData(Pwd.Password);
                        if (pPassword == OldPwd)
                        {
                            sMsg = "Password cannot be the same with your last " + jsox.ParamValue + " passwords";
                            //show_error(MsgTypeEnum.Warning, sMsg);
                            return sMsg;
                        }
                    }
                }
            }

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string ValidasiJSOXUpdate(string pUserID, string pPassword)
    {
        try
        {
            clsJSOXSetup cJSOXSetup = new clsJSOXSetup();
            cJSOXSetup.RuleID = "1";
            clsJSOXSetup jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            int MinimumLength = jsox.ParamValue;
            string sMsg = "";
            if (jsox.Enable && pPassword.Length < MinimumLength)
            {
                sMsg = "Password length minimum " + MinimumLength + " characters";
                return sMsg;
            }

            cJSOXSetup.RuleID = "7";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            if (jsox.Enable == false && pPassword.ToUpper().Contains(pUserID.ToUpper()) == true)
            {
                sMsg = "Password cannot contain User ID";
                return sMsg;
            }

            cJSOXSetup.RuleID = "8";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);

            string pPasswordNumber = string.Empty;

            for (int i = 0; i < pPassword.Length; i++)
            {
                if (Char.IsDigit(pPassword[i]))
                    pPasswordNumber += pPassword[i];
            }

            if (jsox.Enable && pPasswordNumber.Length < jsox.ParamValue)
            {
                sMsg = "Password must contain minimum " + jsox.ParamValue + " numeric";
                return sMsg;
            }

            cJSOXSetup.RuleID = "9";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            if (jsox.Enable == false)
            {
                string PrevChar = pPassword.Substring(0, 1);
                for (int i = 2; i <= pPassword.Length; i++)
                {
                    if (pPassword.Substring(i - 1, 1) == PrevChar)
                    {
                        sMsg = "Password cannot contain repeating characters";
                        return sMsg;
                    }
                    else
                        PrevChar = pPassword.Substring(i - 1, 1);
                }
            }

            cJSOXSetup.RuleID = "5";
            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
            if (jsox.Enable & jsox.ParamValue > 0)
            {
                clsPasswordHistoryDB PasswordHistoryDB = new clsPasswordHistoryDB();
                List<clsPasswordHistory> PassList = PasswordHistoryDB.GetList(pUserID, jsox.ParamValue.ToString().Trim()).ToList();

                Encryption encrypt = new Encryption();

                foreach (var Pwd in PassList)
                {
                    string OldPwd = encrypt.DecryptData(Pwd.Password);
                    if (pPassword == OldPwd)
                    {
                        sMsg = "Password cannot be the same with your last " + jsox.ParamValue + " passwords";
                        return sMsg;
                    }
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public JsonResult UserPrivilege_Sel(string UserID)
    {
        var typeAction = "1"; //GET USER PRIVILEGE SEL
        clsUserPrivilegeDB db = new clsUserPrivilegeDB();
        clsResponse resp = new clsResponse();
        try
        {
            resp = db.UserPrivilegeSel(typeAction, UserID);
        }
        catch (Exception ex)
        {
            resp.Message = ex.Message;
            resp.Contents = "";
        }
        return Json(resp, JsonRequestBehavior.AllowGet);
    }

    public JsonResult UserPrivilege_Upd(List<clsUserPrivilege> data)
    {
        string Message = "";
        string UserLogin = Session["LogUserID"].ToString();
        var typeAction = "2"; //UPDATE USER PRIVILEGE
        clsUserPrivilegeDB db = new clsUserPrivilegeDB();
        clsResponse resp = new clsResponse();
        try
        {
            foreach (var val in data)
            {
                try
                {
                    if (val.Access == true) { val.AllowAccess = "1"; }
                    else { val.AllowAccess = "0"; }

                    if (val.Update == true) val.AllowUpdate = "1";
                    else { val.AllowUpdate = "0"; }

                    resp = db.UserPrivilegeUpd(typeAction, val, UserLogin);

                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    return Json(Message, JsonRequestBehavior.AllowGet);
                }
            }
            Message = "Success Updated data!";
        }
        catch (Exception ex)
        {
            resp.Message = ex.Message;
        }
        return Json(Message, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public JsonResult FillCombo(String TypeAction)
    {
        Response resp = new Response();
        ComboFilter db = new ComboFilter();
        try
        {
            resp = db.FillCombo(TypeAction, "");
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