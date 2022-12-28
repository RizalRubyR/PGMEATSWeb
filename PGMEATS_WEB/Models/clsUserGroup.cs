using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace ADLESKAP.Models
{
    public class clsUserGroup
    {
        [DisplayName("Group ID")]
        [Required]
        public string GroupID { get; set; }

        [DisplayName("Group Description")]
        public string GroupDescription { get; set; }

        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
    }
}