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
                string write = "";
                string userID = Session["LogUserID"] + "";
                ViewBag.UserID = userID;

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "AccessPrivilege/CheckUserPrivilege?UserID=" + userID + "&MenuID=Z-01&AllowType=2").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;

                    if (js.Contains("True"))
                    {
                        write = "True";
                    }
                    else
                    {
                        write = "False";
                    }
                    ViewBag.AllowUpdate = (write == "False") ? "0" : "1";
                }
                else
                {
                    write = "False";
                    ViewBag.AllowUpdate = (write == "False") ? "0" : "1";
                }

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

                string value = string.Empty;
                value = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                var jsonResult = Json(value, JsonRequestBehavior.AllowGet);
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

        //public JsonResult GetListByUserID(string UserID)
        //{
        //    try
        //    {
        //        List<clsUserSetup> users = db.UserListByUserID(UserID).ToList();

        //        string value = string.Empty;
        //        value = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });

        //        var jsonResult = Json(value, JsonRequestBehavior.AllowGet);
        //        jsonResult.MaxJsonLength = int.MaxValue;
        //        return jsonResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = ex.Message, Success = false },
        //            ContentEncoding = System.Text.Encoding.UTF8,
        //            JsonRequestBehavior = JsonRequestBehavior.DenyGet
        //        };
        //    }
        //}

        //public JsonResult InsertData(clsUserSetup user)
        //{
        //    string UserLogin = Session["LogUserID"].ToString();
        //    bool bSuccess;
        //    string msg = "";

        //    try
        //    {
        //        msg = ValidasiJSOXInsert(user.UserID, user.Password);
        //        if (msg == "")
        //        {
        //            db.Insert(user, UserLogin);
        //            db.InsertJSOXHistory(user, UserLogin);
        //            bSuccess = true;
        //            ModelState.Clear();

        //            return new JsonResult
        //            {
        //                Data = new { ErrorMessage = msg, Success = bSuccess },
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }
        //        else
        //        {
        //            bSuccess = false;
        //            ModelState.Clear();

        //            return new JsonResult
        //            {
        //                Data = new { ErrorMessage = msg, Success = bSuccess },
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = ex.Message, Success = false },
        //            ContentEncoding = System.Text.Encoding.UTF8,
        //            JsonRequestBehavior = JsonRequestBehavior.DenyGet
        //        };
        //    }
        //}

        //public JsonResult UpdateData(clsUserSetup user)
        //{
        //    string UserLogin = Session["LogUserID"].ToString();
        //    bool bSuccess;
        //    string msg = "";

        //    try
        //    {
        //        msg = ValidasiJSOXUpdate(user.UserID, user.Password);
        //        if (msg == "")
        //        {
        //            db.Update(user, UserLogin);
        //            db.InsertJSOXHistory(user, UserLogin);
        //            bSuccess = true;
        //            ModelState.Clear();

        //            return new JsonResult
        //            {
        //                Data = new { ErrorMessage = msg, Success = bSuccess },
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }
        //        else
        //        {
        //            bSuccess = false;
        //            ModelState.Clear();

        //            return new JsonResult
        //            {
        //                Data = new { ErrorMessage = msg, Success = bSuccess },
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = ex.Message, Success = false },
        //            ContentEncoding = System.Text.Encoding.UTF8,
        //            JsonRequestBehavior = JsonRequestBehavior.DenyGet
        //        };
        //    }
        //}

        //public JsonResult DeleteData(clsUserSetup user)
        //{
        //    bool bSuccess;
        //    string msg = "";

        //    try
        //    {
        //        db.Delete(user.UserID);
        //        bSuccess = true;
        //        msg = "Delete Data Success";
        //        ModelState.Clear();

        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = msg, Success = bSuccess },
        //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult
        //        {
        //            Data = new { ErrorMessage = ex.Message, Success = false },
        //            ContentEncoding = System.Text.Encoding.UTF8,
        //            JsonRequestBehavior = JsonRequestBehavior.DenyGet
        //        };
        //    }
        //}

        //private string ValidasiJSOXInsert(string pUserID, string pPassword)
        //{
        //    try
        //    {
        //        clsJSOXSetup cJSOXSetup = new clsJSOXSetup();
        //        cJSOXSetup.RuleID = "1";
        //        clsJSOXSetup jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        int MinimumLength = jsox.ParamValue;
        //        string sMsg = "";
        //        if (jsox.Enable && pPassword.Length < MinimumLength)
        //        {
        //            sMsg = "Password length minimum " + MinimumLength + " characters";
        //            return sMsg;
        //        }

        //        cJSOXSetup.RuleID = "7";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        if (jsox.Enable == false && pPassword.ToUpper().Contains(pUserID.ToUpper()) == true)
        //        {
        //            sMsg = "Password cannot contain User ID";
        //            return sMsg;
        //        }

        //        cJSOXSetup.RuleID = "8";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);

        //        string pPasswordNumber = string.Empty;

        //        for (int i = 0; i < pPassword.Length; i++)
        //        {
        //            if (Char.IsDigit(pPassword[i]))
        //                pPasswordNumber += pPassword[i];
        //        }

        //        if (jsox.Enable && pPasswordNumber.Length < jsox.ParamValue)
        //        {
        //            sMsg = "Password must contain minimum " + jsox.ParamValue + " numeric";
        //            return sMsg;
        //        }

        //        cJSOXSetup.RuleID = "9";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        if (jsox.Enable == false)
        //        {
        //            string PrevChar = pPassword.Substring(0, 1);
        //            for (int i = 2; i <= pPassword.Length; i++)
        //            {
        //                if (pPassword.Substring(i - 1, 1) == PrevChar)
        //                {
        //                    sMsg = "Password cannot contain repeating characters";
        //                    return sMsg;
        //                }
        //                else
        //                    PrevChar = pPassword.Substring(i - 1, 1);
        //            }
        //        }

        //        bool PasswordExpired = false;
        //        clsPasswordHistory cPasswordHistory = new clsPasswordHistory();
        //        cPasswordHistory.UserID = pUserID;
        //        clsPasswordHistory His = clsPasswordHistoryDB.GetLastData(cPasswordHistory);
        //        if (His != null)
        //        {
        //            cJSOXSetup = new clsJSOXSetup();
        //            cJSOXSetup.RuleID = "3";
        //            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //            DateTime ServerDate = clsPasswordHistoryDB.GetServerDate();

        //            //int dif = DateDiff(DateInterval.Day, His.UpdateDate, ServerDate);
        //            int dif = int.Parse((ServerDate - His.UpdateDate).Days.ToString());
        //            int expireDay = jsox.ParamValue;
        //            if (dif >= expireDay)
        //            {
        //                cJSOXSetup.RuleID = "2";
        //                jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //                int agingDay = jsox.ParamValue;
        //                if (jsox.Enable)
        //                {
        //                    if (dif >= expireDay)
        //                        PasswordExpired = true;
        //                }
        //            }
        //        }

        //        if (PasswordExpired == true)
        //        {
        //            cJSOXSetup.RuleID = "5";
        //            jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //            if (jsox.Enable & jsox.ParamValue > 0)
        //            {
        //                clsPasswordHistoryDB PasswordHistoryDB = new clsPasswordHistoryDB();
        //                List<clsPasswordHistory> PassList = PasswordHistoryDB.GetList(pUserID, jsox.ParamValue.ToString().Trim()).ToList();

        //                Encryption encrypt = new Encryption();

        //                foreach (var Pwd in PassList)
        //                {
        //                    string OldPwd = encrypt.DecryptData(Pwd.Password.ToString().Trim());
        //                    if (pPassword == OldPwd)
        //                    {
        //                        sMsg = "Password cannot be the same with your last " + jsox.ParamValue + " passwords";
        //                        //show_error(MsgTypeEnum.Warning, sMsg);
        //                        return sMsg;
        //                    }
        //                }
        //            }
        //        }

        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //private string ValidasiJSOXUpdate(string pUserID, string pPassword)
        //{
        //    try
        //    {
        //        clsJSOXSetup cJSOXSetup = new clsJSOXSetup();
        //        cJSOXSetup.RuleID = "1";
        //        clsJSOXSetup jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        int MinimumLength = jsox.ParamValue;
        //        string sMsg = "";
        //        if (jsox.Enable && pPassword.Length < MinimumLength)
        //        {
        //            sMsg = "Password length minimum " + MinimumLength + " characters";
        //            return sMsg;
        //        }

        //        cJSOXSetup.RuleID = "7";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        if (jsox.Enable == false && pPassword.ToUpper().Contains(pUserID.ToUpper()) == true)
        //        {
        //            sMsg = "Password cannot contain User ID";
        //            return sMsg;
        //        }

        //        cJSOXSetup.RuleID = "8";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);

        //        string pPasswordNumber = string.Empty;

        //        for (int i = 0; i < pPassword.Length; i++)
        //        {
        //            if (Char.IsDigit(pPassword[i]))
        //                pPasswordNumber += pPassword[i];
        //        }

        //        if (jsox.Enable && pPasswordNumber.Length < jsox.ParamValue)
        //        {
        //            sMsg = "Password must contain minimum " + jsox.ParamValue + " numeric";
        //            return sMsg;
        //        }

        //        cJSOXSetup.RuleID = "9";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        if (jsox.Enable == false)
        //        {
        //            string PrevChar = pPassword.Substring(0, 1);
        //            for (int i = 2; i <= pPassword.Length; i++)
        //            {
        //                if (pPassword.Substring(i - 1, 1) == PrevChar)
        //                {
        //                    sMsg = "Password cannot contain repeating characters";
        //                    return sMsg;
        //                }
        //                else
        //                    PrevChar = pPassword.Substring(i - 1, 1);
        //            }
        //        }

        //        cJSOXSetup.RuleID = "5";
        //        jsox = clsJSOXSetupDB.GetRule(cJSOXSetup);
        //        if (jsox.Enable & jsox.ParamValue > 0)
        //        {
        //            clsPasswordHistoryDB PasswordHistoryDB = new clsPasswordHistoryDB();
        //            List<clsPasswordHistory> PassList = PasswordHistoryDB.GetList(pUserID, jsox.ParamValue.ToString().Trim()).ToList();

        //            Encryption encrypt = new Encryption();

        //            foreach (var Pwd in PassList)
        //            {
        //                string OldPwd = encrypt.DecryptData(Pwd.Password.ToString().Trim());
        //                if (pPassword == OldPwd)
        //                {
        //                    sMsg = "Password cannot be the same with your last " + jsox.ParamValue + " passwords";
        //                    return sMsg;
        //                }
        //            }
        //        }
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

    }
}