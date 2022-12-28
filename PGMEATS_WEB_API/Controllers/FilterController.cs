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
    public class FilterController : ApiController
    {
        FilterDB db = new FilterDB();

        [HttpGet]
        public IHttpActionResult FilterList(string typefilter)
        {
            try
            {
                var data = new object();

                if (typefilter == "VesselName")
                {
                    data = db.VesselNameList();
                }
                else if (typefilter == "TripType")
                {
                    data = db.TripTypeList();
                }
                else if (typefilter == "SoldTo")
                {
                    data = db.SoldToList();
                }
                else if (typefilter == "Method")
                {
                    data = db.MethodList();
                }
                else if (typefilter == "ETD")
                {
                    data = db.ETDList();
                }
                else if (typefilter == "DistCode")
                {
                    data = db.DistCodeList();
                }
                else if (typefilter == "CFC")
                {
                    data = db.CFCList();
                }
                else if (typefilter == "Brand")
                {
                    data = db.BrandList();
                }
                else if (typefilter == "SJDest")
                {
                    data = db.SJDestList();
                }
                else if (typefilter == "PortOfLoading")
                {
                    data = db.PortOfLoadingList();
                }
                else if (typefilter == "Company")
                {
                    data = db.CompanyList();
                }
                else if (typefilter == "KAPSJClaimPrintStatus" || typefilter == "KAPSJClaimExp" || typefilter == "KAPSJClaimTrip") //Untuk Form KAP Surat Jalan Claim
                {
                    var type = typefilter == "KAPSJClaimPrintStatus" ? "0" : typefilter == "KAPSJClaimExp" ? "1" : "2";
                    data = db.KAPSJClaim(type);
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}