using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGMEATS_WEB.Models
{
    public class clsClinicInformationList
    {
        [RegularExpression("^[a-zA-Z0-9 -]*$", ErrorMessage = "Special character is not allowed")]
        [DisplayName("Region")]
        public string Region { get; set; }

        [RegularExpression("^[a-zA-Z0-9 -]*$", ErrorMessage = "Special character is not allowed")]
        [DisplayName("Clinic Name")]
        [Required]
        public string ClinicName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 -]*$", ErrorMessage = "Special character is not allowed")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("URL")]
        [Required]
        public string URL { get; set; }

        [DisplayName("URL Display")]
        [Required]
        public string URLDisplay { get; set; }

        [DisplayName("LAST UPDATE")]
        public DateTime? LastUpdate { get; set; }

        public string strLastUpdate
        {
            get
            {
                return String.Format("{0:g}", LastUpdate);
            }
        }
        [DisplayName("USER UPDATE")]
        public string UserUpdate { get; set; }
        [DisplayName("USER CREATE")]
        public string UserCreate { get; set; }
        [DisplayName("CREATE DATE")]
        public DateTime? CreateDate { get; set; }
        [DisplayName("UPDATE DATE")]
        public DateTime? UpdateDate { get; set; }
        public string AllowUpdate { get; set; }
        public string AllowCreate { get; set; }
        public string AllowDelete { get; set; }
    }
}