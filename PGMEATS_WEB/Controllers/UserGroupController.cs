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
    public class UserGroupController : Controller
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
                HttpResponseMessage resp = client.GetAsync(uri + "AccessPrivilege/CheckUserPrivilege?UserID=" + userID + "&MenuID=Z-02&AllowType=2").GetAwaiter().GetResult();
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
                HttpResponseMessage resp = client.GetAsync(uri + "UserGroup/UserGroupList").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsUserGroup> users = JsonConvert.DeserializeObject<List<clsUserGroup>>(js);

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

        public JsonResult GetDataByCode(string groupid)
        {
            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "UserGroup/UserGroupListByCode?GroupID=" + WebUtility.UrlEncode(groupid)).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsUserGroup> users = JsonConvert.DeserializeObject<List<clsUserGroup>>(js);

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

        public JsonResult InsertUpdateData(clsUserGroup user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                string UserLogin = Session["LogUserID"].ToString();

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "UserGroup/InsertUpdate?GroupID=" + WebUtility.UrlEncode(user.GroupID)
                                                                          + "&GroupDescription=" + WebUtility.UrlEncode(user.GroupDescription)
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

        public JsonResult DeleteData(clsUserGroup user)
        {
            bool bSuccess;
            string msg = "";

            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "UserGroup/Delete?GroupID=" + WebUtility.UrlEncode(user.GroupID)).GetAwaiter().GetResult();
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
        public ActionResult Detail(string GroupID)
        {
            try
            {
                var model = new UserPrivilegeViewModel();
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "UserGroupPrivilege/UserGroupPrivilegeList?GroupID=" + WebUtility.UrlEncode(GroupID)).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<SelectUserPrivilegeEditorViewModel> privileges = JsonConvert.DeserializeObject<List<SelectUserPrivilegeEditorViewModel>>(js);

                    foreach (SelectUserPrivilegeEditorViewModel pri in privileges)
                    {
                        model.Privileges.Add(pri);
                    }

                    return View(model);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("Detail")]
        public ActionResult Detail_Post(UserPrivilegeViewModel model)
        {
            try
            {
                int i = 0;
                string msgErr = "";

                foreach (SelectUserPrivilegeEditorViewModel pri in model.Privileges)
                {
                    var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage resp = client.GetAsync(uri + "UserGroupPrivilege/Update?GroupID=" + WebUtility.UrlEncode(pri.GroupID)
                                                                                             + "&MenuID=" + WebUtility.UrlEncode(pri.MenuID)
                                                                                             + "&AllowAccess=" + pri.AllowAccess
                                                                                             + "&AllowUpdate=" + pri.AllowUpdate).GetAwaiter().GetResult();
                    if (resp.IsSuccessStatusCode)
                    {
                        i = i + 1;
                    }
                    else
                    {
                        i = 0;
                        msgErr = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                    }
                }

                if (i != 0)
                {
                    TempData["Msg"] = "Updated privilege successfully";
                }
                else
                {
                    TempData["Msg"] = msgErr;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}