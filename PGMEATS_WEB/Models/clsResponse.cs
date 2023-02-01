using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsResponse
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public object Contents { get; set; }
    }
}