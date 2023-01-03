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
    public class HomeController : ApiController
    {
        UserDB db = new UserDB();
       
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult UserCheck(string UserID, string Password)
        {
            try
            {
                UserResponse user = new UserResponse();
                if (db.UserCheck(UserID, Password) > 0)
                {
                    user.RespID = 0;
                    user.RespDesc = "";

                    return Ok(user);
                }
                else
                {
                    user.RespID = 1;
                    user.RespDesc = "User ID or Password is incorrect!";

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
