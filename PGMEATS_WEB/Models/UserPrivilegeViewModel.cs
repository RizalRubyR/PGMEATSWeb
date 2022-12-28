using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADLESKAP.Models
{
   
    public class UserPrivilegeViewModel
    {
        public List<SelectUserPrivilegeEditorViewModel> Privileges { get; set; }
        
        public UserPrivilegeViewModel()
        {
            this.Privileges = new List<SelectUserPrivilegeEditorViewModel>();
        }
    }
}