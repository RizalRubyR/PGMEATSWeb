using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PGMEATS_WEB.Models
{
    /// <summary>
    /// Extend AuthorizeAttribute to correctly handle AJAX authorization
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JsonAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["LogUserID"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Error = "NotAuthorized"
                            //LogOnUrl = FormsAuthentication.LoginUrl
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    // this is a standard request, let parent filter to handle it
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }

        }
    }
}