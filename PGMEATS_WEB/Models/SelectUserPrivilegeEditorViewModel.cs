using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADLESKAP.Models
{
    public class SelectUserPrivilegeEditorViewModel
    {
        public string GroupID { get; set; }
        public string MenuID { get; set; }
        public string MenuGroup { get; set; }
        public string MenuDescription { get; set; }
        public bool AllowAccess { get; set; }
        public bool AllowUpdate { get; set; }
    }
}