using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace PGMEATS_WEB.Models
{
    public class clsClinicInformationList
    {
        public int ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string Remark { get; set; }
        public string URL { get; set; }
        public string URLDisplay { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Phone_No { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string OperationHour { get; set; }

        public string CreateUser { get; set; }
        public string CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateDate { get; set; }
    }
}