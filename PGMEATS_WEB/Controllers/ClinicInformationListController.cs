using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PGMEATS_WEB.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace PGMEATS_WEB.Controllers
{
    public class ClinicInformationListController : Controller
    {
        // GET: ClinicInformationList
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public ActionResult ClinicInformationLists(int draw,int start,int length)
        {
            List<clsClinicInformationList> data = new List<clsClinicInformationList>();
            clsClinicInformationListDB db = new clsClinicInformationListDB();
            clsResponse response = new clsResponse();
            DataTableData dataTableData = new DataTableData();
            try
            {
                //response = db.ClinicInformationList();

                string search = Request.Form.GetValues("search[value]").FirstOrDefault();
                int n = Request.QueryString.Count;
                int sortColumn = -1;
                string sortDirection = "asc";
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                }
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }
                dataTableData.draw = draw;

                clsClinicInformationListDB mdb = new clsClinicInformationListDB();
                List<clsClinicInformationList> models = mdb.ClinicInformationListx.ToList();
                dataTableData.recordsTotal = models.Count;

                int recordsFiltered = 0;
                dataTableData.data = FilterData(models, ref recordsFiltered, start, length, search, sortColumn, sortDirection);
                dataTableData.recordsFiltered = recordsFiltered;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        private List<clsClinicInformationList> FilterData(List<clsClinicInformationList> models, ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        {
            List<clsClinicInformationList> list;
            if (search == null)
            {
                list = models;
            }
            else
            {
                list = new List<clsClinicInformationList>();
                foreach (clsClinicInformationList dataItem in models)
                {
                    if (dataItem.Region.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.Remark.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.ClinicName.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.State.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.Address.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.Phone_No.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.URL.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.URLDisplay.ToUpper().Contains(search.ToUpper())
                       )
                    {
                        list.Add(dataItem);
                    }
                }
            }
            recordFiltered = list.Count;
            list = list.GetRange(start, Math.Min(length, list.Count - start));
            return list;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ClinicInformationIns(clsClinicInformationList data)
        {
            data.CreateUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsClinicInformationListDB DB = new clsClinicInformationListDB();
            try
            {
                response = DB.ClinicInformationIns(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ClinicInformationUpd(clsClinicInformationList data)
        {
            data.UpdateUser = Session["LogUserID"].ToString();
            clsResponse response = new clsResponse();
            clsClinicInformationListDB DB = new clsClinicInformationListDB();
            try
            {
                response = DB.ClinicInformationUpd(data);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ClinicInformationDel(String ClinicID)
        {
            clsResponse response = new clsResponse();
            clsClinicInformationListDB DB = new clsClinicInformationListDB();
            try
            {
                response = DB.ClinicInformationDel(ClinicID);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public JsonResult ClinicInformationValidateBeforeInsert(String pParam)
        {
            var param = pParam.Split('|');
            string Region = param[0];
            string ClinicName = param[1];
            clsResponse response = new clsResponse();
            clsClinicInformationListDB DB = new clsClinicInformationListDB();
            try
            {
                response = DB.ClinicInformationValidateBeforeInsert(Region,ClinicName);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<clsClinicInformationList> data { get; set; }
        }
    }
}