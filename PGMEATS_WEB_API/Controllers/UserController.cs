using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PGMEATS_WEB_API.Models;
using Newtonsoft.Json.Linq;

namespace PGMEATS_WEB_API.Controllers
{
    public class UserController : ApiController
    {
        UserDB db = new UserDB();

        [HttpGet]
        public IHttpActionResult UserList()
        {
            try
            {
                string UserID = "";
                var data = db.UserList(UserID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult UserListByCode(string UserID)
        {
            try
            {
                var data = db.UserList(UserID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult UserTypeList()
        {
            try
            {
                var data = db.UserTypeList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult UserGroupList()
        {
            try
            {
                var data = db.UserGroupList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult InsertUpdate(string UserID, string FullName, string Password, string GroupID, string UserType, string UserLogin)
        {
            User user = new User();
            user.UserID = UserID;
            user.FullName = FullName;
            user.Password = Password;
            user.GroupID = GroupID;
            user.UserType = UserType;

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
        public IHttpActionResult Delete(string UserID)
        {
            try
            {
                db.Delete(UserID);
                return Ok("Delete Data Success");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult ChangePassword(string UserID, string Password)
        {
            ChangePassword password = new ChangePassword();
            password.UserID = UserID;
            password.Password = Password;

            try
            {
                db.ChangePassword(password);
                return Ok("Change Password Success");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}