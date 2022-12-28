using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ADLESKAP_API.Models;
using Newtonsoft.Json.Linq;

namespace ADLESKAP_API.Controllers
{
    public class UserGroupController : ApiController
    {
        UserGroupDB db = new UserGroupDB();

        [HttpGet]
        public IHttpActionResult UserGroupList()
        {
            try
            {
                string GroupID = "";
                var data = db.UserGroupList(GroupID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult UserGroupListByCode(string GroupID)
        {
            try
            {
                var data = db.UserGroupList(GroupID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult InsertUpdate(string GroupID, string GroupDescription, string UserLogin)
        {
            UserGroup user = new UserGroup();
            user.GroupID = GroupID;
            user.GroupDescription = GroupDescription;

            try
            {
                db.Insert(user, UserLogin);
                return Ok("Insert or Update Success");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult Delete(string GroupID)
        {
            try
            {
                db.Delete(GroupID);
                return Ok("Delete Data Success");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}