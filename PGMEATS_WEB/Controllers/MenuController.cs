﻿using PGMEATS_WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;


namespace PGMEATS_WEB.Controllers
{
    [SessionExpire]
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult SideMenu()
        {
            string userID = "";
            try
            {
                if (Session["LogUserID"] != null)
                {
                    userID = Session["LogUserID"].ToString();

                    var uri = new Uri(string.Format(ConfigurationManager.AppSettings["ApiURL"], string.Empty));

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage resp = client.GetAsync(uri + "Menu/MenuList?UserID=" + WebUtility.UrlEncode(userID)).GetAwaiter().GetResult();
                    if (resp.IsSuccessStatusCode)
                    {
                        string js = resp.Content.ReadAsStringAsync().Result;
                        List<clsMenu> menus = JsonConvert.DeserializeObject<List<clsMenu>>(js);

                        return PartialView("_Aside", menus);
                    }
                    else
                    {
                        return RedirectToAction("Home", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Home", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}