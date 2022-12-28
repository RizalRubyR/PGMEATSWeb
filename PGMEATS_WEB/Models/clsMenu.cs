using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADLESKAP.Models
{
    public class clsMenu
    {
        public string MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string MenuGroup { get; set; }
        public int GroupIndex { get; set; }
        public int MenuIndex { get; set; }
        public int AllowAccess { get; set; }
    }
}