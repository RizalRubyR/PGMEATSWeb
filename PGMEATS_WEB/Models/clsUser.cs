using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PGMEATS_WEB.Models
{
    public class clsUser
    {
        [DisplayName("User ID")]
        [Required]
        public string UserID { get; set; }


        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Group ID")]
        public string GroupID { get; set; }

        [DisplayName("User Type")]
        public string UserType { get; set; }

        public DateTime LastLogIn { get; set; }
        public DateTime LastLogOut { get; set; }

        public string LastUser { get; set; }
        public string LastUpdate { get; set; }
        public string AdminStatus { get; set; }


        public string CatererID { get; set; }
        public string Department { get; set; }
    }

    public class clsUserType
    {
        public string UserTypeCode { get; set; }
    }

    public class clsUserGroups
    {
        public string GroupID { get; set; }
        public string GroupDescription { get; set; }
    }

    public class clsChangePassword
    {
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }

    public class clsUserResponse
    {
        public int RespID { get; set; }
        public string RespDesc { get; set; }
    }
}