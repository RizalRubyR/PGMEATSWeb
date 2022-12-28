#region Using

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

#endregion

namespace ADLESKAP
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
           // IdentityConfig.RegisterIdentities();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());  
        }

        protected void Application_BeginRequest()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
        }
    }
}