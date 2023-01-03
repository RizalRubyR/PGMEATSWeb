using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PGMEATS_WEB_API.Models;
using Newtonsoft.Json.Linq;

namespace PGMEATS_WEB_API.Controllers
{
    public class MenuController : ApiController
    {
        MenuDB db = new MenuDB();

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public IHttpActionResult MenuList(string UserID)
        {
            try
            {
                var data = db.MenuList(UserID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}