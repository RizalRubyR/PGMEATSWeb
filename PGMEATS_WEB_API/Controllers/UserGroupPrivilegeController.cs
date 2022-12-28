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
    public class UserGroupPrivilegeController : ApiController
    {
        UserGroupPrivilegeDB db = new UserGroupPrivilegeDB();

        [HttpGet]
        public IHttpActionResult UserGroupPrivilegeList(string GroupID)
        {
            try
            {
                var data = db.UserGroupPrivilegeList(GroupID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult Update(string GroupID, string MenuID, bool AllowAccess, bool AllowUpdate)
        {
            UserGroupPrivilege user = new UserGroupPrivilege();
            user.GroupID = GroupID;
            user.MenuID = MenuID;
            user.AllowAccess = AllowAccess;
            user.AllowUpdate = AllowUpdate;

            try
            {
                db.UpdatePrivilege(user);
                return Ok("Update Success");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}