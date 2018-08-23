using iHoaDon.Entities;
using iHoaDon.Resources.Admin;
using iHoaDon.Resources.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iHoaDon.Web.Areas.Admin.Models
{
    public class AccountModel
    {
        /// <summary>
        /// gets or sets the title
        /// </summary>
        [StringLength(250, ErrorMessageResourceName = "LoginNameMaxLength", ErrorMessageResourceType = typeof(AccountModelResource))]
        [LocalizedDisplayName("LoginName", NameResourceType = typeof(AccountModelResource))]
        [Required(ErrorMessageResourceName = "LoginNameRequied", ErrorMessageResourceType = typeof(AccountModelResource))]
        public string CompanyCode { get; set; }

        /// <summary>
        /// gets or sets the content
        /// </summary>
        [LocalizedDisplayName("Password", NameResourceType = typeof(AccountModelResource))]
        public string Password { get; set; }

        /// <summary>
        /// gets or sets the lead
        /// </summary>
        [StringLength(250, ErrorMessageResourceName = "FullNameMaxLength", ErrorMessageResourceType = typeof(AccountModelResource))]
        [LocalizedDisplayName("FullName", NameResourceType = typeof(AccountModelResource))]
        public string CompanyName { get; set; }



        public string RoleName { get; set; }

        [LocalizedDisplayName("Role", NameResourceType = typeof(AccountModelResource))]
        public int Role { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(256, ErrorMessageResourceName = "EmailMaxLength", ErrorMessageResourceType = typeof(AccountModelResource))]
        [LocalizedDisplayName("Email", NameResourceType = typeof(AccountModelResource))]
        [Required(ErrorMessageResourceName = "EmailRequied", ErrorMessageResourceType = typeof(AccountModelResource))]
        public string Phone { get; set; }



    }
}