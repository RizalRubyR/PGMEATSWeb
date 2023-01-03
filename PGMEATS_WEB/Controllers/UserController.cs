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
    [SessionExpire]
    public class UserController : Controller
    {
        [HttpGet]
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

        //for list grid
        public JsonResult GetList()
        {
            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/UserList").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsUser> users = JsonConvert.DeserializeObject<List<clsUser>>(js);

                    var jsonResult = Json(users, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { ErrorMessage = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result), Success = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.DenyGet
                    };
                }
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

        //for combo User Type
        public JsonResult GetComboUserType()
        {
            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/UserTypeList").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsUserType> userTypes = JsonConvert.DeserializeObject<List<clsUserType>>(js);

                    return Json(new SelectList(userTypes, "UserTypeCode", "UserTypeCode"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { ErrorMessage = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result), Success = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.DenyGet
                    };
                }
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

        //for combo User Group
        public JsonResult GetComboUserGroup()
        {
            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/UserGroupList").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsUserGroups> userGroups = JsonConvert.DeserializeObject<List<clsUserGroups>>(js);

                    return Json(new SelectList(userGroups, "GroupID", "GroupDescription"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { ErrorMessage = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result), Success = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.DenyGet
                    };
                }
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

        public JsonResult GetDataByCode(string userid)
        {
            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/UserListByCode?UserID=" + WebUtility.UrlEncode(userid)).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsUser> users = JsonConvert.DeserializeObject<List<clsUser>>(js);

                    string value = string.Empty;
                    value = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                    var jsonResult = Json(value, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { ErrorMessage = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result), Success = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.DenyGet
                    };
                }
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

        public JsonResult InsertUpdateData(clsUser user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                string UserLogin = Session["LogUserID"].ToString();

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/InsertUpdate?UserID=" + user.UserID
                                                                          + "&FullName=" + WebUtility.UrlEncode(user.FullName) 
                                                                          + "&Password=" + user.Password
                                                                          + "&GroupID=" + WebUtility.UrlEncode(user.GroupID)
                                                                          + "&UserType=" + WebUtility.UrlEncode(user.UserType)
                                                                          + "&UserLogin=" + WebUtility.UrlEncode(UserLogin)).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    bSuccess = true;
                    msg = resp.StatusCode.ToString();
                    ModelState.Clear();
                }
                else
                {
                    bSuccess = false;
                    msg = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                    ModelState.Clear();
                }

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

        public JsonResult DeleteData(clsUser user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/Delete?UserID=" + user.UserID).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    bSuccess = true;
                    msg = resp.StatusCode.ToString();
                    ModelState.Clear();
                }
                else
                {
                    bSuccess = false;
                    msg = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                    ModelState.Clear();
                }

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

        [HttpGet]
        public ActionResult ChangePassword()
        {
            string userID = Session["LogUserID"].ToString();
            ViewBag.UserID = userID;

            return View();
        }

        public JsonResult UpdatePassword(clsChangePassword user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "User/ChangePassword?UserID=" + user.UserID + "&Password=" + user.Password).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    bSuccess = true;
                    msg = resp.StatusCode.ToString();
                    ModelState.Clear();
                }
                else
                {
                    bSuccess = false;
                    msg = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                    ModelState.Clear();
                }

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