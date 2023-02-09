using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsClinicInformationList
    {
        public int ClinicID { get; set; }
        public string Region { get; set; }
        public string ClinicName { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string URLDisplay { get; set; }
        public string CreateUser { get; set; }
        public string CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public string UpdateDate { get; set; }


    }
}