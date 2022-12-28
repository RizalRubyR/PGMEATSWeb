using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using ADLESKAP.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.Web.Routing;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;

namespace ADLESKAP.Controllers
{
    public class HomeController : Controller
    {        
        [SessionExpire]
        [SessionTimeout]
        public ActionResult Index()
        {
            string userID = Session["LogUserID"] + "";
            ViewBag.UserID = userID;

            return View();
        }

        public ActionResult _JobAssignment()
        {
            string userID = Session["LogUserID"] + "";
            ViewBag.UserID = userID;

            if (Session["LogUserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonJobAssignment").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonJobAssignment> data = JsonConvert.DeserializeObject<List<clsAndonJobAssignment>>(js);

                    ViewBag.AndonJobAssigment = data.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Job Assignment - " + JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                }


                var client2 = new HttpClient();
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp2 = client2.GetAsync(uri + "Home/AndonJobAssignmentCount").GetAwaiter().GetResult();
                if (resp2.IsSuccessStatusCode)
                {
                    string js2 = resp2.Content.ReadAsStringAsync().Result;
                    List<clsAndonJobAssignmentCount> data2 = JsonConvert.DeserializeObject<List<clsAndonJobAssignmentCount>>(js2);

                    ViewBag.AndonJobAssigmentCount = data2.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Job Assignment Active Job - " + JsonConvert.DeserializeObject<dynamic>(resp2.Content.ReadAsStringAsync().Result);
                }


                string viewName = "_JobAssignment";
                return PartialView(viewName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult _KAPOperation()
        {
            string userID = Session["LogUserID"] + "";
            ViewBag.UserID = userID;

            if (Session["LogUserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperation").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonKAPOperation> data = JsonConvert.DeserializeObject<List<clsAndonKAPOperation>>(js);

                    ViewBag.AndonKAPOperation = data.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon KAP Operations - " + JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                }

                var client2 = new HttpClient();
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp2 = client2.GetAsync(uri + "Home/AndonKAPOperationMP").GetAwaiter().GetResult();
                if (resp2.IsSuccessStatusCode)
                {
                    string js2 = resp2.Content.ReadAsStringAsync().Result;
                    List<clsAndonKAPOperationMP> data2 = JsonConvert.DeserializeObject<List<clsAndonKAPOperationMP>>(js2);

                    ViewBag.AndonKAPOperationMP = data2.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Manpower Performance Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp2.Content.ReadAsStringAsync().Result);
                }

                string viewName = "_KAPOperation";
                return PartialView(viewName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult _PatimbanOperation()
        {
            string userID = Session["LogUserID"] + "";
            ViewBag.UserID = userID;

            if (Session["LogUserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonPatimbanOperationClaimPort").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperationClaimPort> data = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperationClaimPort>>(js);

                    ViewBag.MonitoringClaimPort = data.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations Monitoring Claim Port - " + JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                }

                var client2 = new HttpClient();
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp2 = client2.GetAsync(uri + "Home/AndonPatimbanOperationPortStock").GetAwaiter().GetResult();
                if (resp2.IsSuccessStatusCode)
                {
                    string js2 = resp2.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperationPortStock> data2 = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperationPortStock>>(js2);

                    ViewBag.PortStock = data2.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations Port Stock Status Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp2.Content.ReadAsStringAsync().Result);
                }

                var client3 = new HttpClient();
                client3.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp3 = client3.GetAsync(uri + "Home/AndonPatimbanOperationCaseMark").GetAwaiter().GetResult();
                if (resp3.IsSuccessStatusCode)
                {
                    string js3 = resp3.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperationCaseMark> data3 = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperationCaseMark>>(js3);

                    ViewBag.CaseMark = data3.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations Case Mark Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp3.Content.ReadAsStringAsync().Result);
                }

                var client4 = new HttpClient();
                client4.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp4 = client4.GetAsync(uri + "Home/AndonPatimbanOperationPortOut").GetAwaiter().GetResult();
                if (resp4.IsSuccessStatusCode)
                {
                    string js4 = resp4.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperationPortOut> data4 = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperationPortOut>>(js4);

                    ViewBag.PortOut = data4.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations Port Out Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp4.Content.ReadAsStringAsync().Result);
                }

                var client5 = new HttpClient();
                client5.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp5 = client5.GetAsync(uri + "Home/AndonPatimbanOperation").GetAwaiter().GetResult();
                if (resp5.IsSuccessStatusCode)
                {
                    string js5 = resp5.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperation> data5 = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperation>>(js5);

                    ViewBag.AndonPatimbanOperation = data5.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations - " + JsonConvert.DeserializeObject<dynamic>(resp5.Content.ReadAsStringAsync().Result);
                }

                var client6 = new HttpClient();
                client6.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp6 = client6.GetAsync(uri + "Home/AndonPatimbanOperationOutToPatimban?PlantID=SAP").GetAwaiter().GetResult();
                if (resp6.IsSuccessStatusCode)
                {
                    string js6 = resp6.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperationOutToPatimban> data6 = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperationOutToPatimban>>(js6);

                    ViewBag.AndonPatimbanOperationVLCOutToPatimban = data6.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations Out To Patimban - " + JsonConvert.DeserializeObject<dynamic>(resp6.Content.ReadAsStringAsync().Result);
                }

                var client7 = new HttpClient();
                client7.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp7 = client7.GetAsync(uri + "Home/AndonPatimbanOperationOutToPatimban?PlantID=KAP").GetAwaiter().GetResult();
                if (resp7.IsSuccessStatusCode)
                {
                    string js7 = resp7.Content.ReadAsStringAsync().Result;
                    List<clsAndonPatimbanOperationOutToPatimban> data7 = JsonConvert.DeserializeObject<List<clsAndonPatimbanOperationOutToPatimban>>(js7);

                    ViewBag.AndonPatimbanOperationKAPOutToPatimban = data7.ToList();
                }
                else
                {
                    TempData["Msg"] = "Failed to load data Andon Patimban Operations Out To Patimban - " + JsonConvert.DeserializeObject<dynamic>(resp7.Content.ReadAsStringAsync().Result);
                }

                string viewName = "_PatimbanOperation";
                return PartialView(viewName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public ActionResult _CCOperation()
        //{
        //    string userID = Session["LogUserID"] + "";
        //    ViewBag.UserID = userID;

        //    if (Session["LogUserID"] == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }

        //    try
        //    {
        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));
        //        //CC Delivery Monitoring
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonCCOperationDelMonitoring").GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            string js = resp.Content.ReadAsStringAsync().Result;
        //            List<clsAndonCCDeliveryMonitoring> data = JsonConvert.DeserializeObject<List<clsAndonCCDeliveryMonitoring>>(js);

        //            ViewBag.AndonCCOperationDelMonitoring = data.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations Delivery Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
        //        }
        //        //Volume Adjustment Priok
        //        var client2 = new HttpClient();
        //        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp2 = client2.GetAsync(uri + "Home/AndonCCOperationVAPriok").GetAwaiter().GetResult();
        //        if (resp2.IsSuccessStatusCode)
        //        {
        //            string js2 = resp2.Content.ReadAsStringAsync().Result;
        //            List<clsAndonCCVolumeAdjustmentPriok> data2 = JsonConvert.DeserializeObject<List<clsAndonCCVolumeAdjustmentPriok>>(js2);

        //            ViewBag.AndonCCOperationVAPriok = data2.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations Volume Adjustment T. Priok - " + JsonConvert.DeserializeObject<dynamic>(resp2.Content.ReadAsStringAsync().Result);
        //        }
        //        //Volume Adjustment Patimban
        //        var client3 = new HttpClient();
        //        client3.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp3 = client3.GetAsync(uri + "Home/AndonCCOperationVAPatimban").GetAwaiter().GetResult();
        //        if (resp3.IsSuccessStatusCode)
        //        {
        //            string js3 = resp3.Content.ReadAsStringAsync().Result;
        //            List<clsAndonCCVolumeAdjustmentPatimban> data3 = JsonConvert.DeserializeObject<List<clsAndonCCVolumeAdjustmentPatimban>>(js3);

        //            ViewBag.AndonCCOperationVAPatimban = data3.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations Volume Adjustment Patimban - " + JsonConvert.DeserializeObject<dynamic>(resp3.Content.ReadAsStringAsync().Result);
        //        }
        //        //Volume Yard Monitoring
        //        var client4 = new HttpClient();
        //        client4.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp4 = client4.GetAsync(uri + "Home/AndonCCOperationVYMonitoring").GetAwaiter().GetResult();
        //        if (resp4.IsSuccessStatusCode)
        //        {
        //            string js4 = resp4.Content.ReadAsStringAsync().Result;
        //            List<clsAndonCCVolumeYardMonitoring> data4 = JsonConvert.DeserializeObject<List<clsAndonCCVolumeYardMonitoring>>(js4);

        //            ViewBag.AndonCCOperationVYMonitoring = data4.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations Volume Yard Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp4.Content.ReadAsStringAsync().Result);
        //        }
        //        //CC Performance
        //        var client5 = new HttpClient();
        //        client5.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp5 = client5.GetAsync(uri + "Home/AndonCCOperationCCPerformance").GetAwaiter().GetResult();
        //        if (resp5.IsSuccessStatusCode)
        //        {
        //            string js5 = resp5.Content.ReadAsStringAsync().Result;
        //            List<clsAndonCCPerformance> data5 = JsonConvert.DeserializeObject<List<clsAndonCCPerformance>>(js5);

        //            ViewBag.AndonCCOperationCCPerformance = data5.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations CC Performance - " + JsonConvert.DeserializeObject<dynamic>(resp5.Content.ReadAsStringAsync().Result);
        //        }
        //        //Single CC Delivery Monitoring
        //        var client6 = new HttpClient();
        //        client6.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp6 = client6.GetAsync(uri + "Home/AndonCCOperationSingleCCDelMonitoring").GetAwaiter().GetResult();
        //        if (resp6.IsSuccessStatusCode)
        //        {
        //            string js6 = resp6.Content.ReadAsStringAsync().Result;
        //            List<clsAndonSingleCCDeliveryMonitoring> data6 = JsonConvert.DeserializeObject<List<clsAndonSingleCCDeliveryMonitoring>>(js6);

        //            ViewBag.AndonCCOperationSingleCCDelMonitoring = data6.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations Single CC Delivery Monitoring - " + JsonConvert.DeserializeObject<dynamic>(resp6.Content.ReadAsStringAsync().Result);
        //        }
        //        //Single CC Performance
        //        var client7 = new HttpClient();
        //        client7.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp7 = client7.GetAsync(uri + "Home/AndonCCOperationSingleCCPerformance").GetAwaiter().GetResult();
        //        if (resp7.IsSuccessStatusCode)
        //        {
        //            string js7 = resp7.Content.ReadAsStringAsync().Result;
        //            List<clsAndonSingleCCPerformance> data7 = JsonConvert.DeserializeObject<List<clsAndonSingleCCPerformance>>(js7);

        //            ViewBag.AndonCCOperationSingleCCPerformance = data7.ToList();
        //        }
        //        else
        //        {
        //            TempData["Msg"] = "Failed to load data Andon CC Operations CC Single Performance - " + JsonConvert.DeserializeObject<dynamic>(resp7.Content.ReadAsStringAsync().Result);
        //        }

        //        string viewName = "_CCOperation";
        //        return PartialView(viewName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public JsonResult CalcDelMonitoring(string SAPPlantID, string SAPVAOut, string SAPRunning, string SAPAssy, string SAPPainting,
        //                                    string KAPPlantID, string KAPVAOut, string KAPRunning, string KAPAssy, string KAPPainting)
        //{
        //    bool bSuccess;
        //    int notSuccess = 0;
        //    string tmpmsgOK = "";
        //    string tmpmsgNG = "";
        //    string msg = "";

        //    string UserLogin = Session["LogUserID"].ToString();

        //    try
        //    {
        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

        //        if (SAPPlantID == "1")
        //        {
        //            string data = "";
        //            if (SAPVAOut == "VA")
        //            {
        //                data = data + SAPVAOut + ",";
        //            }

        //            if (SAPRunning == "Running")
        //            {
        //                data = data + SAPRunning + ",";
        //            }

        //            if (SAPAssy == "Assy")
        //            {
        //                data = data + SAPAssy + ",";
        //            }

        //            if (SAPPainting == "Painting")
        //            {
        //                data = data + SAPPainting + ",";
        //            }

        //            data = data.Substring(0, data.Length - 1);

        //            var splitdata = data.Split(',');
        //            for (int i = 0; i < splitdata.Count(); i++)
        //            {
        //                string Area = splitdata[i];

        //                var client = new HttpClient();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage resp = client.GetAsync(uri + "Home/CalcDelMonitoring?PlantID=SAP"
        //                                                                  + "&Area=" + WebUtility.UrlEncode(Area)
        //                                                                  + "&UserLogin=" + WebUtility.UrlEncode(UserLogin)).GetAwaiter().GetResult();

        //                if (resp.IsSuccessStatusCode)
        //                {
        //                    notSuccess = notSuccess + 0;
        //                    tmpmsgOK = resp.StatusCode.ToString();
        //                    ModelState.Clear();
        //                }
        //                else
        //                {
        //                    notSuccess = notSuccess + 1;
        //                    tmpmsgNG = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
        //                    ModelState.Clear();
        //                }
        //            }
        //        }

        //        if (KAPPlantID == "1")
        //        {
        //            string data = "";
        //            if (KAPVAOut == "VA")
        //            {
        //                data = data + KAPVAOut + ",";
        //            }

        //            if (KAPRunning == "Running")
        //            {
        //                data = data + KAPRunning + ",";
        //            }

        //            if (KAPAssy == "Assy")
        //            {
        //                data = data + KAPAssy + ",";
        //            }

        //            if (KAPPainting == "Painting")
        //            {
        //                data = data + KAPPainting + ",";
        //            }

        //            data = data.Substring(0, data.Length - 1);

        //            var splitdata = data.Split(',');
        //            for (int i = 0; i < splitdata.Count(); i++)
        //            {
        //                string Area = splitdata[i];

        //                var client = new HttpClient();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage resp = client.GetAsync(uri + "Home/CalcDelMonitoring?PlantID=KAP"
        //                                                                  + "&Area=" + WebUtility.UrlEncode(Area)
        //                                                                  + "&UserLogin=" + WebUtility.UrlEncode(UserLogin)).GetAwaiter().GetResult();

        //                if (resp.IsSuccessStatusCode)
        //                {
        //                    notSuccess = notSuccess + 0;
        //                    tmpmsgOK = resp.StatusCode.ToString();
        //                    ModelState.Clear();
        //                }
        //                else
        //                {
        //                    notSuccess = notSuccess + 1;
        //                    tmpmsgNG = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
        //                    ModelState.Clear();
        //                }
        //            }
        //        }

        //        if (notSuccess > 0)
        //        {
        //            bSuccess = false;
        //            msg = tmpmsgNG;
        //        }
        //        else
        //        {
        //            bSuccess = true;
        //            msg = tmpmsgOK;
        //        }

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

        //public JsonResult CalcVolumeAdjust(string sdataPriok, string sdataPatimban)
        //{
        //    bool bSuccess;
        //    int notSuccess = 0;
        //    string tmpmsgOK = "";
        //    string tmpmsgNG = "";
        //    string msg = "";

        //    string UserLogin = Session["LogUserID"].ToString();

        //    var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

        //    try
        //    {
        //        if (sdataPriok == null)
        //        {
        //            sdataPriok = "";
        //        }
        //        if (sdataPatimban == null)
        //        {
        //            sdataPatimban = "";
        //        }

        //        if (sdataPriok != "")
        //        {
        //            var splitdataPriok = sdataPriok.Split(',');
        //            for (int i = 0; i < splitdataPriok.Count(); i++)
        //            {
        //                var paramPriok = splitdataPriok[i];
        //                string[] data = paramPriok.Split('|');
                        
        //                string Route = data[0];
        //                string ETD = data[1];
        //                string CountryCode = data[2];
        //                string Method = data[3];
        //                string Model = data[4];
        //                int Qty = Convert.ToInt32(data[5]);

        //                var client = new HttpClient();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage resp = client.GetAsync(uri + "Home/CalcVolumeAdjustment?Route=" + WebUtility.UrlEncode(Route)
        //                                                                  + "&ETD=" + WebUtility.UrlEncode(ETD)
        //                                                                  + "&CountryCode=" + WebUtility.UrlEncode(CountryCode)
        //                                                                  + "&Method=" + WebUtility.UrlEncode(Method)
        //                                                                  + "&Model=" + WebUtility.UrlEncode(Model)
        //                                                                  + "&Qty=" + Qty
        //                                                                  + "&UserLogin=" + WebUtility.UrlEncode(UserLogin)).GetAwaiter().GetResult();

        //                if (resp.IsSuccessStatusCode)
        //                {
        //                    notSuccess = notSuccess + 0;
        //                    tmpmsgOK = resp.StatusCode.ToString();
        //                    ModelState.Clear();
        //                }
        //                else
        //                {
        //                    notSuccess = notSuccess + 1;
        //                    tmpmsgNG = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
        //                    ModelState.Clear();
        //                }
        //            }
        //        }

        //        if (sdataPatimban != "")
        //        {
        //            var splitdataPatimban = sdataPatimban.Split(',');
        //            for (int i = 0; i < splitdataPatimban.Count(); i++)
        //            {
        //                var paramPatimban = splitdataPatimban[i];
        //                string[] data = paramPatimban.Split('|');

        //                string Route = data[0];
        //                string ETD = data[1];
        //                string CountryCode = data[2];
        //                string Method = data[3];
        //                string Model = data[4];
        //                int Qty = Convert.ToInt32(data[5]);

        //                var client = new HttpClient();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage resp = client.GetAsync(uri + "Home/CalcVolumeAdjustment?Route=" + Route
        //                                                                  + "&ETD=" + WebUtility.UrlEncode(ETD)
        //                                                                  + "&CountryCode=" + WebUtility.UrlEncode(CountryCode)
        //                                                                  + "&Method=" + WebUtility.UrlEncode(Method)
        //                                                                  + "&Model=" + WebUtility.UrlEncode(Model)
        //                                                                  + "&Qty=" + Qty
        //                                                                  + "&UserLogin=" + WebUtility.UrlEncode(UserLogin)).GetAwaiter().GetResult();

        //                if (resp.IsSuccessStatusCode)
        //                {
        //                    notSuccess = notSuccess + 0;
        //                    tmpmsgOK = resp.StatusCode.ToString();
        //                    ModelState.Clear();
        //                }
        //                else
        //                {
        //                    notSuccess = notSuccess + 1;
        //                    tmpmsgNG = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
        //                    ModelState.Clear();
        //                }
        //            }
        //        }

        //        if (notSuccess > 0)
        //        {
        //            bSuccess = false;
        //            msg = tmpmsgNG;
        //        }
        //        else
        //        {
        //            bSuccess = true;
        //            msg = tmpmsgOK;
        //        }
                

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

        public ActionResult DownloadClaimNGMonitoring()
        {
            try
            {
                List<clsAndoKAPExcelClaimNGMonitoring> rpt = new List<clsAndoKAPExcelClaimNGMonitoring>();

                using (ExcelPackage exl = new ExcelPackage())
                {
                    ExcelWorksheet ws = exl.Workbook.Worksheets.Add("Sheet1");
                    rpt = GetDataClaimNGMonitoring();

                    if (rpt.Count == 0)
                    {
                        TempData["Msg"] = "Data not Found";
                    }
                    else
                    {
                        //Header
                        ws.Cells["A2"].Value = "CLAIM & NG VEHICLE MONITORING";
                        ws.Cells["A2"].Style.Font.Bold = true;
                        ws.Cells["A2"].Style.Font.Name = "Calibri";
                        ws.Cells["A2"].Style.Font.Size = 14;

                        ws.Cells["A4"].Value = "No";
                        ws.Cells["B4"].Value = "VIN";
                        ws.Cells["C4"].Value = "Katashiki";
                        ws.Cells["D4"].Value = "Sfx";
                        ws.Cells["E4"].Value = "Color";
                        ws.Cells["F4"].Value = "ETD";
                        ws.Cells["G4"].Value = "Methode";
                        ws.Cells["H4"].Value = "Port of Loading";
                        ws.Cells["I4"].Value = "Vessel";
                        ws.Cells["J4"].Value = "Country";
                        ws.Cells["K4"].Value = "NG Status";
                        ws.Cells["L4"].Value = "Defect Name";
                        ws.Cells["M4"].Value = "Last Location";
                        ws.Cells["N4"].Value = "Last Time Update";
                        ws.Cells["A4:N4"].Style.Font.Name = "Calibri";
                        ws.Cells["A4:N4"].Style.Font.Size = 11;

                        ws.Column(1).Width = 5;
                        ws.Column(2).Width = 20;
                        ws.Column(3).Width = 15;
                        ws.Column(4).Width = 8;
                        ws.Column(5).Width = 8;
                        ws.Column(6).Width = 12;
                        ws.Column(7).Width = 12;
                        ws.Column(8).Width = 20;
                        ws.Column(9).Width = 20;
                        ws.Column(10).Width = 20;
                        ws.Column(11).Width = 12;
                        ws.Column(12).Width = 45;
                        ws.Column(13).Width = 17;
                        ws.Column(14).Width = 17;

                        ExcelRange rgHead = ws.Cells[4, 1, 4, 14];
                        rgHead.Style.WrapText = true;
                        rgHead.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rgHead.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rgHead.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rgHead.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 255, 153));
                        rgHead.Style.Font.Bold = true;

                        //Detail
                        int i = 5;
                        foreach (clsAndoKAPExcelClaimNGMonitoring model in rpt)
                        {
                            ws.Cells[i, 1].Value = model.No;
                            ws.Cells[i, 2].Value = model.VIN;
                            ws.Cells[i, 3].Value = model.Katashiki;
                            ws.Cells[i, 4].Value = model.Suffix;
                            ws.Cells[i, 5].Value = model.Color;
                            ws.Cells[i, 6].Value = model.ETD;
                            ws.Cells[i, 7].Value = model.Method;
                            ws.Cells[i, 8].Value = model.PortOfLoading;
                            ws.Cells[i, 9].Value = model.Vessel;
                            ws.Cells[i, 10].Value = model.Country;
                            ws.Cells[i, 11].Value = model.NGStatus;
                            ws.Cells[i, 12].Value = model.DefectName;
                            ws.Cells[i, 13].Value = model.LastLocation;
                            ws.Cells[i, 14].Value = model.LastTimeUpdate;
                            i = i + 1;
                        }

                        ExcelRange rg = ws.Cells[4, 1, i - 1, 14];
                        rg.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Top.Style = ExcelBorderStyle.Thin;

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=ClaimNGMonitoring_" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + ".xlsx");

                        MemoryStream stream = new MemoryStream(exl.GetAsByteArray());
                        Response.OutputStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
                        Response.Flush();
                        Response.Close();
                    }


                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DownloadOnProgressDelivery()
        {
            try
            {
                DataTable result = new DataTable();
                result = GetDataOnProgressDelivery();

                using (XLWorkbook wb = new XLWorkbook())
                {
                    if (result.Rows.Count == 0)
                    {
                        TempData["Msg"] = "Data not Found";
                    }
                    else
                    {
                        int rowCount = result.Rows.Count;

                        IXLWorksheet ws = wb.Worksheets.Add("Sheet1");
                        //Header
                        ws.Cell("A2").Value = "ON PROGRESS DELIVERY TO PORT SUMMARY";

                        ws.Cell("A2").Style.Font.Bold = true;
                        ws.Cell("A2").Style.Font.FontName = "Calibri";
                        ws.Cell("A2").Style.Font.FontSize = 14;
                        ws.Range("A2:M2").Merge();
                        ws.Range("A2:M2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        ws.Cell("A4").Value = "Rute";
                        ws.Cell("B4").Value = "Qty Truck ";
                        ws.Cell("C4").Value = "Truck No";
                        ws.Cell("D4").Value = "Driver";
                        ws.Cell("E4").Value = "Company";
                        ws.Cell("F4").Value = "Qty Unit";
                        ws.Cell("G4").Value = "Route";
                        ws.Cell("H4").Value = "SJ No";
                        ws.Cell("I4").Value = "E-Ticket";
                        ws.Cell("J4").Value = "ETD";
                        ws.Cell("K4").Value = "Country";
                        ws.Cell("L4").Value = "Method";
                        ws.Cell("M4").Value = "VIN";

                        ws.Range("A4:M4").Style.Font.FontName = "Calibri";
                        ws.Range("A4:M4").Style.Font.FontSize = 11;
                        ws.Range("A4:M4").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 255, 153);
                        ws.Range("A4:M4").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Range("A4:M4").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        ws.Range("A4:M4").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        ws.Range("A4:M4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Range("A4:M4").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Range("A4:M4").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Range("A4:M4").Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        ws.Column(1).Width = 20;
                        ws.Column(2).Width = 10;
                        ws.Column(3).Width = 14;
                        ws.Column(4).Width = 19;
                        ws.Column(5).Width = 23;
                        ws.Column(6).Width = 10;
                        ws.Column(7).Width = 15;
                        ws.Column(8).Width = 20;
                        ws.Column(9).Width = 20;
                        ws.Column(10).Width = 15;
                        ws.Column(11).Width = 15;
                        ws.Column(12).Width = 15;
                        ws.Column(13).Width = 22;

                        //Detail
                        int rowStart = 5;

                        int rowFirst = 0;
                        int rowFirstTruck = 0;
                        int rowFirstQtyTruck = 0;
                        int rowFirstDriver = 0;
                        int rowFirstSJNo = 0;

                        string pStatus = "";
                        int pQtyTruck = 0;
                        string pTruckNo = "";
                        string pDriver = "";
                        string pCompany = "";
                        int pQtyUnit = 0;
                        string pRoute = "";
                        string pSJNo = "";

                        for (int i = 0; i < rowCount; i++)
                        {
                            if (pStatus != result.Rows[i][0].ToString())
                            {
                                ws.Cell(i + rowStart, 1).Value = result.Rows[i][0].ToString();
                                rowFirst = i + rowStart;
                            }

                            if (pQtyTruck != Convert.ToInt32(result.Rows[i][1].ToString()))
                            {
                                ws.Cell(i + rowStart, 2).Value = Convert.ToInt32(result.Rows[i][1].ToString());
                                rowFirstQtyTruck = i + rowStart;
                            }

                            if (pTruckNo != result.Rows[i][2].ToString())
                            {
                                ws.Cell(i + rowStart, 3).Value = result.Rows[i][2].ToString();
                                rowFirstTruck = i + rowStart;
                            }

                            if ((pDriver != result.Rows[i][3].ToString()) && (pTruckNo != result.Rows[i][2].ToString()))
                            {
                                ws.Cell(i + rowStart, 4).Value = result.Rows[i][3].ToString();
                                ws.Cell(i + rowStart, 5).Value = result.Rows[i][4].ToString();
                                ws.Cell(i + rowStart, 6).Value = Convert.ToInt32(result.Rows[i][5].ToString());
                                ws.Cell(i + rowStart, 7).Value = result.Rows[i][6].ToString();
                                rowFirstDriver = i + rowStart;
                            }

                            if ((pCompany != result.Rows[i][4].ToString()) && (pDriver != result.Rows[i][3].ToString()))
                            {
                                ws.Cell(i + rowStart, 5).Value = result.Rows[i][4].ToString();
                            }

                            if ((pQtyUnit != Convert.ToInt32(result.Rows[i][5].ToString())) && (pSJNo != result.Rows[i][7].ToString()))
                            {
                                ws.Cell(i + rowStart, 6).Value = Convert.ToInt32(result.Rows[i][5].ToString());
                            }

                            if ((pRoute != result.Rows[i][6].ToString()) && (pSJNo != result.Rows[i][7].ToString()))
                            {
                                ws.Cell(i + rowStart, 7).Value = result.Rows[i][6].ToString();
                            }

                            if (pSJNo != result.Rows[i][7].ToString())
                            {
                                ws.Cell(i + rowStart, 8).Value = result.Rows[i][7].ToString();
                                ws.Cell(i + rowStart, 9).Value = result.Rows[i][8].ToString();
                                rowFirstSJNo = i + rowStart;
                            }

                            ws.Cell(i + rowStart, 10).Value = result.Rows[i][9].ToString();
                            ws.Cell(i + rowStart, 10).Style.DateFormat.Format = "dd MMM yyyy";

                            ws.Cell(i + rowStart, 11).Value = result.Rows[i][10].ToString();
                            ws.Cell(i + rowStart, 12).Value = result.Rows[i][11].ToString();
                            ws.Cell(i + rowStart, 13).Value = result.Rows[i][12].ToString();

                            pStatus = result.Rows[i][0].ToString();
                            pQtyTruck = Convert.ToInt32(result.Rows[i][1].ToString());
                            pTruckNo = result.Rows[i][2].ToString();
                            pDriver = result.Rows[i][3].ToString();
                            pCompany = result.Rows[i][4].ToString();
                            pQtyUnit = Convert.ToInt32(result.Rows[i][5].ToString());
                            pRoute = result.Rows[i][6].ToString();
                            pSJNo = result.Rows[i][7].ToString();

                            ws.Range(ws.Cell(rowFirst, 1), ws.Cell(i + rowStart, 1)).Merge();
                            ws.Range(ws.Cell(rowFirstQtyTruck, 2), ws.Cell(i + rowStart, 2)).Merge();
                            ws.Range(ws.Cell(rowFirstTruck, 3), ws.Cell(i + rowStart, 3)).Merge();
                            ws.Range(ws.Cell(rowFirstDriver, 4), ws.Cell(i + rowStart, 4)).Merge();
                            ws.Range(ws.Cell(rowFirstDriver, 5), ws.Cell(i + rowStart, 5)).Merge();
                            ws.Range(ws.Cell(rowFirstDriver, 6), ws.Cell(i + rowStart, 6)).Merge();
                            ws.Range(ws.Cell(rowFirstDriver, 7), ws.Cell(i + rowStart, 7)).Merge();
                            ws.Range(ws.Cell(rowFirstSJNo, 8), ws.Cell(i + rowStart, 8)).Merge();
                            ws.Range(ws.Cell(rowFirstSJNo, 9), ws.Cell(i + rowStart, 9)).Merge();

                            if (result.Rows[i][13].ToString() == "HIJAU")
                            {
                                ws.Range(ws.Cell(i + rowStart, 10), ws.Cell(i + rowStart, 13)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 255, 153);
                            }

                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            ws.Range(ws.Cell(i + rowStart, 1), ws.Cell(i + rowStart, 13)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=OnProgressDelivery_" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //try
            //{
            //    List<clsAndonPatimbanExcelOnProgressDelivery> rpt = new List<clsAndonPatimbanExcelOnProgressDelivery>();

            //    using (ExcelPackage exl = new ExcelPackage())
            //    {
            //        ExcelWorksheet ws = exl.Workbook.Worksheets.Add("Sheet1");
            //        rpt = GetDataOnProgressDelivery();

            //        if (rpt.Count == 0)
            //        {
            //            TempData["Msg"] = "Data not Found";
            //        }
            //        else
            //        {
            //            //Header
            //            ws.Cells["A2"].Value = "ON PROGRESS DELIVERY TO PORT SUMMARY";
            //            ws.Cells["A2"].Style.Font.Bold = true;
            //            ws.Cells["A2"].Style.Font.Name = "Calibri";
            //            ws.Cells["A2"].Style.Font.Size = 14;

            //            ws.Cells["A4"].Value = "Rute";
            //            ws.Cells["B4"].Value = "Qty Truck ";
            //            ws.Cells["C4"].Value = "Truck No";
            //            ws.Cells["D4"].Value = "Driver";
            //            ws.Cells["E4"].Value = "Company";
            //            ws.Cells["F4"].Value = "Qty Unit";
            //            ws.Cells["G4"].Value = "Route";
            //            ws.Cells["H4"].Value = "SJ No";
            //            ws.Cells["I4"].Value = "E-Ticket";
            //            ws.Cells["J4"].Value = "ETD";
            //            ws.Cells["K4"].Value = "Country";
            //            ws.Cells["L4"].Value = "Method";
            //            ws.Cells["M4"].Value = "VIN";
            //            ws.Cells["A4:M4"].Style.Font.Name = "Calibri";
            //            ws.Cells["A4:M4"].Style.Font.Size = 11;

            //            ws.Column(1).Width = 20;
            //            ws.Column(2).Width = 10;
            //            ws.Column(3).Width = 14;
            //            ws.Column(4).Width = 19;
            //            ws.Column(5).Width = 23;
            //            ws.Column(6).Width = 10;
            //            ws.Column(7).Width = 15;
            //            ws.Column(8).Width = 20;
            //            ws.Column(9).Width = 20;
            //            ws.Column(10).Width = 15;
            //            ws.Column(11).Width = 15;
            //            ws.Column(12).Width = 15;
            //            ws.Column(13).Width = 22;

            //            ExcelRange rgHead = ws.Cells[4, 1, 4, 13];
            //            rgHead.Style.WrapText = true;
            //            rgHead.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //            rgHead.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //            rgHead.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //            rgHead.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 255, 153));
            //            rgHead.Style.Font.Bold = true;

            //            //Detail
            //            int rowStart = 5;

            //            int rowFirst = 0;
            //            int rowFirstTruck = 0;
            //            int rowFirstQtyTruck = 0;
            //            int rowFirstDriver = 0;
            //            int rowFirstSJNo = 0;

            //            string pStatus = "";
            //            int pQtyTruck = 0;
            //            string pTruckNo = "";
            //            string pDriver = "";
            //            string pCompany = "";
            //            int pQtyUnit = 0;
            //            string pRoute = "";
            //            string pSJNo = "";

            //            for (int i = 0; i < 1010; i++)
            //            {
            //                if (pStatus != rpt[i].Rute)
            //                {
            //                    ws.Cells[i + rowStart, 1].Value = rpt[i].Rute;
            //                    rowFirst = i + rowStart;
            //                }

            //                if (pQtyTruck != rpt[i].QtyTruck)
            //                {
            //                    ws.Cells[i + rowStart, 2].Value = rpt[i].QtyTruck;
            //                    rowFirstQtyTruck = i + rowStart;
            //                }

            //                if (pTruckNo != rpt[i].TruckNo)
            //                {
            //                    ws.Cells[i + rowStart, 3].Value = rpt[i].TruckNo;
            //                    rowFirstTruck = i + rowStart;
            //                }

            //                if ((pDriver != rpt[i].Driver) && (pTruckNo != rpt[i].TruckNo))
            //                {
            //                    ws.Cells[i + rowStart, 4].Value = rpt[i].Driver;
            //                    ws.Cells[i + rowStart, 5].Value = rpt[i].Company;
            //                    ws.Cells[i + rowStart, 6].Value = rpt[i].QtyUnit;
            //                    ws.Cells[i + rowStart, 7].Value = rpt[i].Route;
            //                    rowFirstDriver = i + rowStart;
            //                }

            //                if ((pCompany != rpt[i].Company) && (pDriver != rpt[i].Driver))
            //                {
            //                    ws.Cells[i + rowStart, 5].Value = rpt[i].Company;
            //                }

            //                if ((pQtyUnit != rpt[i].QtyUnit) && (pSJNo != rpt[i].SJNo))
            //                {
            //                    ws.Cells[i + rowStart, 6].Value = rpt[i].QtyUnit;
            //                }

            //                if ((pRoute != rpt[i].Route) && (pSJNo != rpt[i].SJNo))
            //                {
            //                    ws.Cells[i + rowStart, 7].Value = rpt[i].Route;
            //                }

            //                if (pSJNo != rpt[i].SJNo)
            //                {
            //                    ws.Cells[i + rowStart, 8].Value = rpt[i].SJNo;
            //                    ws.Cells[i + rowStart, 9].Value = rpt[i].ETicket;
            //                    rowFirstSJNo = i + rowStart;
            //                }

            //                ws.Cells[i + rowStart, 10].Value = rpt[i].ETD;
            //                ws.Cells[i + rowStart, 11].Value = rpt[i].Country;
            //                ws.Cells[i + rowStart, 12].Value = rpt[i].Method;
            //                ws.Cells[i + rowStart, 13].Value = rpt[i].VIN;

            //                pStatus = rpt[i].Rute;
            //                pQtyTruck = rpt[i].QtyTruck;
            //                pTruckNo = rpt[i].TruckNo;
            //                pDriver = rpt[i].Driver;
            //                pCompany = rpt[i].Company;
            //                pQtyUnit = rpt[i].QtyUnit;
            //                pRoute = rpt[i].Route;
            //                pSJNo = rpt[i].SJNo;

            //                ws.Cells[rowFirst, 1, i + rowStart, 1].Merge = true;
            //                ws.Cells[rowFirstQtyTruck, 2, i + rowStart, 2].Merge = true;
            //                ws.Cells[rowFirstTruck, 3, i + rowStart, 3].Merge = true;
            //                ws.Cells[rowFirstDriver, 4, i + rowStart, 4].Merge = true;
            //                ws.Cells[rowFirstDriver, 5, i + rowStart, 5].Merge = true;
            //                ws.Cells[rowFirstDriver, 6, i + rowStart, 6].Merge = true;
            //                ws.Cells[rowFirstDriver, 7, i + rowStart, 7].Merge = true;
            //                ws.Cells[rowFirstSJNo, 8, i + rowStart, 8].Merge = true;
            //                ws.Cells[rowFirstSJNo, 9, i + rowStart, 9].Merge = true;

            //                if (rpt[i].Status == "HIJAU")
            //                {
            //                    ExcelRange rgDetail = ws.Cells[i + rowStart, 10, i + rowStart, 13];
            //                    rgDetail.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //                    rgDetail.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 255, 153));
            //                }

            //            }

            //            ExcelRange rg = ws.Cells[4, 1, rpt.Count + 4, 13];
            //            rg.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //            rg.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //            rg.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //            rg.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //            rg.Style.Border.Top.Style = ExcelBorderStyle.Thin;

            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            Response.AddHeader("content-disposition", "attachment; filename=OnProgressDelivery_" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + ".xlsx");

            //            MemoryStream stream = new MemoryStream(exl.GetAsByteArray());
            //            Response.OutputStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
            //            Response.Flush();
            //            Response.Close();
            //        }
            //    }
            //    return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    return View(ex.Message, JsonRequestBehavior.AllowGet);
            //}
        }

        public ActionResult DownloadClaimPortMonitoring()
        {
            try
            {
                List<clsAndoPatimbanExcelClaimPortMonitoring> rpt = new List<clsAndoPatimbanExcelClaimPortMonitoring>();

                using (ExcelPackage exl = new ExcelPackage())
                {
                    ExcelWorksheet ws = exl.Workbook.Worksheets.Add("Sheet1");
                    rpt = GetDataClaimPortMonitoring();

                    if (rpt.Count == 0)
                    {
                        TempData["Msg"] = "Data not Found";
                    }
                    else
                    {
                        //Header
                        ws.Cells["A2"].Value = "Monitoring Claim Port (No Drop Document)";
                        ws.Cells["A2"].Style.Font.Bold = true;
                        ws.Cells["A2"].Style.Font.Name = "Calibri";
                        ws.Cells["A2"].Style.Font.Size = 14;

                        ws.Cells["A4"].Value = "No";
                        ws.Cells["B4"].Value = "VIN";
                        ws.Cells["C4"].Value = "Katashiki";
                        ws.Cells["D4"].Value = "Sfx";
                        ws.Cells["E4"].Value = "Color";
                        ws.Cells["F4"].Value = "ETD";
                        ws.Cells["G4"].Value = "Methode";
                        ws.Cells["H4"].Value = "Port of Loading";
                        ws.Cells["I4"].Value = "Vessel";
                        ws.Cells["J4"].Value = "Country";
                        ws.Cells["K4"].Value = "NG Status";
                        ws.Cells["L4"].Value = "Defect Name";
                        ws.Cells["M4"].Value = "Last Location";
                        ws.Cells["N4"].Value = "Last Time Update";
                        ws.Cells["A4:N4"].Style.Font.Name = "Calibri";
                        ws.Cells["A4:N4"].Style.Font.Size = 11;

                        ws.Column(1).Width = 5;
                        ws.Column(2).Width = 20;
                        ws.Column(3).Width = 15;
                        ws.Column(4).Width = 8;
                        ws.Column(5).Width = 8;
                        ws.Column(6).Width = 12;
                        ws.Column(7).Width = 12;
                        ws.Column(8).Width = 20;
                        ws.Column(9).Width = 20;
                        ws.Column(10).Width = 20;
                        ws.Column(11).Width = 12;
                        ws.Column(12).Width = 45;
                        ws.Column(13).Width = 17;
                        ws.Column(14).Width = 17;

                        ExcelRange rgHead = ws.Cells[4, 1, 4, 14];
                        rgHead.Style.WrapText = true;
                        rgHead.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rgHead.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rgHead.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rgHead.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 255, 153));
                        rgHead.Style.Font.Bold = true;

                        //Detail
                        int i = 5;
                        foreach (clsAndoPatimbanExcelClaimPortMonitoring model in rpt)
                        {
                            ws.Cells[i, 1].Value = model.No;
                            ws.Cells[i, 2].Value = model.VIN;
                            ws.Cells[i, 3].Value = model.Katashiki;
                            ws.Cells[i, 4].Value = model.Suffix;
                            ws.Cells[i, 5].Value = model.Color;
                            ws.Cells[i, 6].Value = model.ETD;
                            ws.Cells[i, 7].Value = model.Method;
                            ws.Cells[i, 8].Value = model.PortOfLoading;
                            ws.Cells[i, 9].Value = model.Vessel;
                            ws.Cells[i, 10].Value = model.Country;
                            ws.Cells[i, 11].Value = model.NGStatus;
                            ws.Cells[i, 12].Value = model.DefectName;
                            ws.Cells[i, 13].Value = model.LastLocation;
                            ws.Cells[i, 14].Value = model.LastTimeUpdate;
                            i = i + 1;
                        }

                        ExcelRange rg = ws.Cells[4, 1, i - 1, 14];
                        rg.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rg.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rg.AutoFitColumns();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=ClaimNGMonitoring_" + Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") + ".xlsx");

                        MemoryStream stream = new MemoryStream(exl.GetAsByteArray());
                        Response.OutputStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
                        Response.Flush();
                        Response.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public List<clsAndoKAPExcelClaimNGMonitoring> GetDataClaimNGMonitoring()
        {
            try
            {
                List<clsAndoKAPExcelClaimNGMonitoring> lists = new List<clsAndoKAPExcelClaimNGMonitoring>();
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/GetDataClaimNGMonitoring").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    lists = JsonConvert.DeserializeObject<List<clsAndoKAPExcelClaimNGMonitoring>>(js);
                }
                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDataOnProgressDelivery()
        {
            try
            {
                DataTable result = new DataTable();

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(30);
                HttpResponseMessage resp = client.GetAsync(uri + "Home/GetDataOnProgressDelivery").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<DataTable>(js);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<clsAndonPatimbanExcelOnProgressDelivery> GetDataOnProgressDelivery()
        //{
        //    try
        //    {
        //        List<clsAndonPatimbanExcelOnProgressDelivery> lists = new List<clsAndonPatimbanExcelOnProgressDelivery>();
        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));
        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.Timeout = TimeSpan.FromMinutes(30);
        //        HttpResponseMessage resp = client.GetAsync(uri + "Home/GetDataOnProgressDelivery").GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            string js = resp.Content.ReadAsStringAsync().Result;
        //            lists = JsonConvert.DeserializeObject<List<clsAndonPatimbanExcelOnProgressDelivery>>(js);
        //        }
        //        return lists;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<clsAndoPatimbanExcelClaimPortMonitoring> GetDataClaimPortMonitoring()
        {
            try
            {
                List<clsAndoPatimbanExcelClaimPortMonitoring> lists = new List<clsAndoPatimbanExcelClaimPortMonitoring>();
                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/GetDataClaimPortMonitoring").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    lists = JsonConvert.DeserializeObject<List<clsAndoPatimbanExcelClaimPortMonitoring>>(js);
                }
                return lists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ContentResult VAOutChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=VAOut").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "red";

                            }
                            else if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#ffffff";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult YardInChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=YardIn").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#fff";

                            }
                            else if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "red";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult ShippingLineChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=SL").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#fff";

                            }
                            else if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "red";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult PIOChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=PIO").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#464653";

                            }
                            else if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "aqua";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult TanjungPriokChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=Priok").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#fff";

                            }
                            else if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "red";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult PatimbanChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=Patimban").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#fff";

                            }
                            else if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "orange";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult ResultDeliveryChart()
        {
            try
            {
                DataChartKAP chartData = new DataChartKAP();
                string pcolor = "";

                var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonKAPOperationChart?Type=ResultDelivery").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string js = resp.Content.ReadAsStringAsync().Result;
                    List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

                    string[] tmplabel = new string[10];
                    decimal[] tmpVal = new decimal[10];
                    string[] tmpColor = new string[10];

                    int tmpCount = 0;
                    int counter = 0;

                    if (data.Count != 0)
                    {
                        for (int x = 0; x < data.Count; x++)
                        {
                            tmplabel[counter] = data[x].LabelName;
                            tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

                            if (tmplabel[counter] == "ActualPlan")
                            {
                                pcolor = "red";

                            }
                            else if (tmplabel[counter] == "OriginalPlan")
                            {
                                pcolor = "#464653";
                            }

                            tmpColor[counter] = pcolor.Trim();

                            counter++;
                            tmpCount++;
                        }
                    }

                    string[] clabel = new string[tmpCount];
                    string[] cColor = new string[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount = tmpCount - 1;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            clabel[i] = tmplabel[i];
                            cColor[i] = tmpColor[i];
                        }
                    }

                    chartData.labels = clabel;

                    tmpCount++;
                    decimal[] series1 = new decimal[tmpCount];
                    if (tmpCount > 0)
                    {
                        tmpCount--;
                        for (int i = 0; i <= tmpCount; i++)
                        {
                            series1[i] = tmpVal[i];
                        }
                    }

                    chartData.datasets = new List<DatasetsKAP>();
                    List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

                    _dataSet.Add(new DatasetsKAP()
                    {
                        fill = false,
                        data = series1,
                        borderColor = cColor,
                        backgroundColor = cColor,
                        borderWidth = "1",
                    });

                    chartData.datasets = _dataSet;

                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
                }
                else
                {
                    JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
                }

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        //[HttpPost]
        //public ContentResult SAPDelMonitoringChart()
        //{
        //    try
        //    {
        //        DataChartKAP chartData = new DataChartKAP();
        //        string pcolor = "";

        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonCCOperationChart?Type=SAPDelMonitoring").GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            string js = resp.Content.ReadAsStringAsync().Result;
        //            List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

        //            string[] tmplabel = new string[10];
        //            decimal[] tmpVal = new decimal[10];
        //            string[] tmpColor = new string[10];

        //            int tmpCount = 0;
        //            int counter = 0;

        //            if (data.Count != 0)
        //            {
        //                for (int x = 0; x < data.Count; x++)
        //                {
        //                    tmplabel[counter] = data[x].LabelName;
        //                    tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

        //                    if (tmplabel[counter] == "ActualPlan")
        //                    {
        //                        pcolor = "#0aef51";

        //                    }
        //                    else if (tmplabel[counter] == "OriginalPlan")
        //                    {
        //                        pcolor = "#00893b";
        //                    }

        //                    tmpColor[counter] = pcolor.Trim();

        //                    counter++;
        //                    tmpCount++;
        //                }
        //            }

        //            string[] clabel = new string[tmpCount];
        //            string[] cColor = new string[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount = tmpCount - 1;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    clabel[i] = tmplabel[i];
        //                    cColor[i] = tmpColor[i];
        //                }
        //            }

        //            chartData.labels = clabel;

        //            tmpCount++;
        //            decimal[] series1 = new decimal[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount--;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    series1[i] = tmpVal[i];
        //                }
        //            }

        //            chartData.datasets = new List<DatasetsKAP>();
        //            List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

        //            _dataSet.Add(new DatasetsKAP()
        //            {
        //                fill = false,
        //                data = series1,
        //                borderColor = cColor,
        //                backgroundColor = cColor,
        //                borderWidth = "1",
        //            });

        //            chartData.datasets = _dataSet;

        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        //        }
        //        else
        //        {
        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.Exception(ex.Message);
        //    }
        //}

        //[HttpPost]
        //public ContentResult KAPDelMonitoringChart()
        //{
        //    try
        //    {
        //        DataChartKAP chartData = new DataChartKAP();
        //        string pcolor = "";

        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonCCOperationChart?Type=KAPDelMonitoring").GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            string js = resp.Content.ReadAsStringAsync().Result;
        //            List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

        //            string[] tmplabel = new string[10];
        //            decimal[] tmpVal = new decimal[10];
        //            string[] tmpColor = new string[10];

        //            int tmpCount = 0;
        //            int counter = 0;

        //            if (data.Count != 0)
        //            {
        //                for (int x = 0; x < data.Count; x++)
        //                {
        //                    tmplabel[counter] = data[x].LabelName;
        //                    tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

        //                    if (tmplabel[counter] == "ActualPlan")
        //                    {
        //                        pcolor = "#0aef51";

        //                    }
        //                    else if (tmplabel[counter] == "OriginalPlan")
        //                    {
        //                        pcolor = "#00893b";
        //                    }

        //                    tmpColor[counter] = pcolor.Trim();

        //                    counter++;
        //                    tmpCount++;
        //                }
        //            }

        //            string[] clabel = new string[tmpCount];
        //            string[] cColor = new string[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount = tmpCount - 1;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    clabel[i] = tmplabel[i];
        //                    cColor[i] = tmpColor[i];
        //                }
        //            }

        //            chartData.labels = clabel;

        //            tmpCount++;
        //            decimal[] series1 = new decimal[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount--;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    series1[i] = tmpVal[i];
        //                }
        //            }

        //            chartData.datasets = new List<DatasetsKAP>();
        //            List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

        //            _dataSet.Add(new DatasetsKAP()
        //            {
        //                fill = false,
        //                data = series1,
        //                borderColor = cColor,
        //                backgroundColor = cColor,
        //                borderWidth = "1",
        //            });

        //            chartData.datasets = _dataSet;

        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        //        }
        //        else
        //        {
        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.Exception(ex.Message);
        //    }
        //}

        //[HttpPost]
        //public ContentResult DelToVLCChart()
        //{
        //    try
        //    {
        //        DataChartKAP chartData = new DataChartKAP();
        //        string pcolor = "";

        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonCCOperationChart?Type=DelToVLC").GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            string js = resp.Content.ReadAsStringAsync().Result;
        //            List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

        //            string[] tmplabel = new string[10];
        //            decimal[] tmpVal = new decimal[10];
        //            string[] tmpColor = new string[10];

        //            int tmpCount = 0;
        //            int counter = 0;

        //            if (data.Count != 0)
        //            {
        //                for (int x = 0; x < data.Count; x++)
        //                {
        //                    tmplabel[counter] = data[x].LabelName;
        //                    tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

        //                    if (tmplabel[counter] == "ActualPlan")
        //                    {
        //                        pcolor = "#0aef51";

        //                    }
        //                    else if (tmplabel[counter] == "OriginalPlan")
        //                    {
        //                        pcolor = "#00893b";
        //                    }

        //                    tmpColor[counter] = pcolor.Trim();

        //                    counter++;
        //                    tmpCount++;
        //                }
        //            }

        //            string[] clabel = new string[tmpCount];
        //            string[] cColor = new string[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount = tmpCount - 1;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    clabel[i] = tmplabel[i];
        //                    cColor[i] = tmpColor[i];
        //                }
        //            }

        //            chartData.labels = clabel;

        //            tmpCount++;
        //            decimal[] series1 = new decimal[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount--;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    series1[i] = tmpVal[i];
        //                }
        //            }

        //            chartData.datasets = new List<DatasetsKAP>();
        //            List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

        //            _dataSet.Add(new DatasetsKAP()
        //            {
        //                fill = false,
        //                data = series1,
        //                borderColor = cColor,
        //                backgroundColor = cColor,
        //                borderWidth = "1",
        //            });

        //            chartData.datasets = _dataSet;

        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        //        }
        //        else
        //        {
        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.Exception(ex.Message);
        //    }
        //}

        //[HttpPost]
        //public ContentResult DirectDelChart()
        //{
        //    try
        //    {
        //        DataChartKAP chartData = new DataChartKAP();
        //        string pcolor = "";

        //        var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

        //        var client = new HttpClient();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage resp = client.GetAsync(uri + "Home/AndonCCOperationChart?Type=DirectDel").GetAwaiter().GetResult();
        //        if (resp.IsSuccessStatusCode)
        //        {
        //            string js = resp.Content.ReadAsStringAsync().Result;
        //            List<clsAndonValue> data = JsonConvert.DeserializeObject<List<clsAndonValue>>(js);

        //            string[] tmplabel = new string[10];
        //            decimal[] tmpVal = new decimal[10];
        //            string[] tmpColor = new string[10];

        //            int tmpCount = 0;
        //            int counter = 0;

        //            if (data.Count != 0)
        //            {
        //                for (int x = 0; x < data.Count; x++)
        //                {
        //                    tmplabel[counter] = data[x].LabelName;
        //                    tmpVal[counter] = Convert.ToDecimal(data[x].LabelValue.ToString());

        //                    if (tmplabel[counter] == "ActualPlan")
        //                    {
        //                        pcolor = "#0aef51";

        //                    }
        //                    else if (tmplabel[counter] == "OriginalPlan")
        //                    {
        //                        pcolor = "#00893b";
        //                    }

        //                    tmpColor[counter] = pcolor.Trim();

        //                    counter++;
        //                    tmpCount++;
        //                }
        //            }

        //            string[] clabel = new string[tmpCount];
        //            string[] cColor = new string[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount = tmpCount - 1;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    clabel[i] = tmplabel[i];
        //                    cColor[i] = tmpColor[i];
        //                }
        //            }

        //            chartData.labels = clabel;

        //            tmpCount++;
        //            decimal[] series1 = new decimal[tmpCount];
        //            if (tmpCount > 0)
        //            {
        //                tmpCount--;
        //                for (int i = 0; i <= tmpCount; i++)
        //                {
        //                    series1[i] = tmpVal[i];
        //                }
        //            }

        //            chartData.datasets = new List<DatasetsKAP>();
        //            List<DatasetsKAP> _dataSet = new List<DatasetsKAP>();

        //            _dataSet.Add(new DatasetsKAP()
        //            {
        //                fill = false,
        //                data = series1,
        //                borderColor = cColor,
        //                backgroundColor = cColor,
        //                borderWidth = "1",
        //            });

        //            chartData.datasets = _dataSet;

        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject(chartData, _jsonSetting), "application/json");
        //        }
        //        else
        //        {
        //            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //            return Content(JsonConvert.SerializeObject("", _jsonSetting), "application/json");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.Exception(ex.Message);
        //    }
        //}

        [HttpGet]
        public ActionResult Login(string logout)
        {
            if (logout == "1" || logout != null)
            {
                Session["LogUserID"] = null;
                Session.Clear();
                Session.Abandon();
            }

            if (logout == "1")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(clsUser User)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage resp = client.GetAsync(uri + "Home/UserCheck?UserID=" + WebUtility.UrlEncode(User.UserID) + "&Password=" + WebUtility.UrlEncode(User.Password)).GetAwaiter().GetResult();
                    if (resp.IsSuccessStatusCode)
                    {

                        string js = resp.Content.ReadAsStringAsync().Result;
                        clsUserResponse data = JsonConvert.DeserializeObject<clsUserResponse>(js);

                        if (data.RespID == 0)
                        {
                            message = "Success";
                            Session["LogUserID"] = User.UserID.ToString();
                        }
                        else
                        {
                            message = "User ID or Password is incorrect!";
                        }
                    }
                    else
                    {
                        message = JsonConvert.DeserializeObject<dynamic>(resp.Content.ReadAsStringAsync().Result);
                    }
                }
                catch(Exception ex)
                {
                    message = ex.Message.ToString().Trim();
                }
            }
            else
            {
                if (User.UserID == null)
                {
                    message = "Please input User ID!";
                }
                else if (User.Password == null)
                {
                    message = "Please input Password!";
                }
            }

            ViewBag.Message = message;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Error500(string Error_Message)
        {
            ViewBag.Error_Message = Error_Message;
            return View();
        }
    }
}