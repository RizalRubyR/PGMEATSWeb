using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ADLESKAP_API.Models;
using Newtonsoft.Json.Linq;

namespace ADLESKAP_API.Controllers
{
    public class HomeController : ApiController
    {
        UserDB db = new UserDB();
        AndonKAPOperationDB adb = new AndonKAPOperationDB();
        AndonJobAssignmentDB bdb = new AndonJobAssignmentDB();
        AndonPatimbanOperationDB cdb = new AndonPatimbanOperationDB();
        AndonCCOperationDB ddb = new AndonCCOperationDB();

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

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonKAPOperation()
        {
            try
            {
                var data = adb.GetAndonKAPOperations();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonKAPOperationMP()
        {
            try
            {
                var data = adb.GetAndonKAPOperationsMP();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonKAPOperationChart(string Type)
        {
            try
            {
                var data = adb.GetAndonKAPOperationsChart(Type);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult GetDataClaimNGMonitoring()
        {
            try
            {
                var data = adb.GetExcelClaimNGMonitoring();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult GetDataOnProgressDelivery()
        {
            try
            {
                var data = cdb.GetExcelOnProgressDelivery();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public IHttpActionResult GetDataClaimPortMonitoring()
        {
            try
            {
                var data = cdb.GetExcelClaimPortMonitoring();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonPatimbanOperation()
        {
            try
            {
                var data = cdb.GetAndonPatimbanOperations();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonPatimbanOperationOutToPatimban(string PlantID)
        {
            try
            {
                var data = cdb.AndonPatimbanOperationOutToPatimban(PlantID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonPatimbanOperationClaimPort()
        {
            try
            {
                var data = cdb.GetAndonPatimbanOperationClaimPort();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonPatimbanOperationPortStock()
        {
            try
            {
                var data = cdb.GetAndonPatimbanOperationPortStock();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonPatimbanOperationCaseMark()
        {
            try
            {
                var data = cdb.GetAndonPatimbanOperationCaseMark();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonPatimbanOperationPortOut()
        {
            try
            {
                var data = cdb.GetAndonPatimbanOperationPortOut();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationDelMonitoring()
        {
            try
            {
                var data = ddb.GetAndonCCDelMonitoring();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationVAPriok()
        {
            try
            {
                var data = ddb.GetAndonCCVAPriok();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationVAPatimban()
        {
            try
            {
                var data = ddb.GetAndonCCVAPatimban();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationVYMonitoring()
        {
            try
            {
                var data = ddb.GetAndonCCVYMonitoring();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationCCPerformance()
        {
            try
            {
                var data = ddb.GetAndonCCPerformance();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationSingleCCDelMonitoring()
        {
            try
            {
                var data = ddb.GetAndonSingleCCDelMonitoring();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationSingleCCPerformance()
        {
            try
            {
                var data = ddb.GetAndonSingleCCPerformance();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonCCOperationChart(string Type)
        {
            try
            {
                var data = ddb.GetAndonCCOperationsChart(Type);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult CalcDelMonitoring(string PlantID, string Area, string UserLogin)
        {
            try
            {
                ddb.InsertCalcDelMonitoring(PlantID, Area, UserLogin);
                return Ok("Insert Data Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult CalcVolumeAdjustment(string Route, string ETD, string CountryCode, string Method, string Model, int Qty, string UserLogin)
        {
            try
            {
                ddb.InsertCalcVolumeAdjust(Route, ETD, CountryCode, Method, Model, Qty, UserLogin);
                return Ok("Insert Data Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonJobAssignment()
        {
            try
            {
                var data = bdb.GetAndonJobAssignment();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult AndonJobAssignmentCount()
        {
            try
            {
                var data = bdb.GetAndonJobAssignmentCount();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        AndonKAPOperationDB KAP = new AndonKAPOperationDB();
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult InsertData(string Qty, string Counted, string Achieve, string CreateUser, string ActualStatus)
        {
            AndonSTO_History History = new AndonSTO_History();
            History.QTY_HISTORY = Qty;
            History.COUNTED_HISTORY = Counted;
            History.ACHIEVE_HISTORY = Achieve;
            History.CREATE_USER = CreateUser;
            History.ActualStatus = ActualStatus;

            try
            {
                KAP.InsertData(History);
                return Ok("Insert Data Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult UpdateStatus(string istatus)
        {

            try
            {
                KAP.UpdateStatus(istatus);
                return Ok("Update Data Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
