#region Using

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace PGMEATS_WEB
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*(CrystalImageHandler).*" });
            routes.IgnoreRoute("CrystalImageHandler.aspx/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                //controller = "Home",
                //action = "Login",
                controller = "Home",
                action = "Login",
                id = UrlParameter.Optional
            }).RouteHandler = new DashRouteHandler();
        }
    }
}