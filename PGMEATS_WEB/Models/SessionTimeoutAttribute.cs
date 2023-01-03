using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGMEATS_WEB.Models
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["LogUserID"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login",true);
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }  
}