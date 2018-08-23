using iHoaDon.Entities;
using iHoaDon.Resources.Admin.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iHoaDon.Web.Areas.Admin.Models
{
    public class LogOnModel
    {
        [StringLength(32, ErrorMessageResourceName = "LoginNameMaxLength", ErrorMessageResourceType = typeof(LogOnModelResources))]
        [LocalizedDisplayName("LoginName", NameResourceType = typeof(LogOnModelResources))]
        [Required(ErrorMessageResourceName = "LoginNameRequied", ErrorMessageResourceType = typeof(LogOnModelResources))]
        public string LoginName { get; set; }

        [StringLength(32, ErrorMessageResourceName = "PassWordMaxLength", ErrorMessageResourceType = typeof(LogOnModelResources))]
        [LocalizedDisplayName("PassWord", NameResourceType = typeof(LogOnModelResources))]
        [Required(ErrorMessageResourceName = "PassWordRequied", ErrorMessageResourceType = typeof(LogOnModelResources))]
        public string PassWord { get; set; }

        [LocalizedDisplayName("Remember", NameResourceType = typeof(LogOnModelResources))]
        public bool Remember { get; set; }
    }
}