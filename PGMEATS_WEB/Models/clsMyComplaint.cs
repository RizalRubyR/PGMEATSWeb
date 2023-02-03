using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsMyComplaint
    {
        public string ComplaintID { get; set; }
        public string IssueTypeID { get; set; }
        public string ComplaintDesc { get; set; }
        public string ComplaintReplay { get; set; }
        public string ComplaintStatus { get; set; }
        public string SessionID { get; set; }
        public string LocationID { get; set; }
        public string Evidence { get; set; }
        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
    }

    public class clsMyComplaintDB
    {

    }

}