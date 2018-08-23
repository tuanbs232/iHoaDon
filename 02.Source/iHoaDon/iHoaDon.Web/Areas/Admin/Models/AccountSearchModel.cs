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
    public class AccountSearchModel
    {

        /// <summary>
        /// gets or sets the title
        /// </summary>
        [LocalizedDisplayName("LoginName", NameResourceType = typeof(AccountSearchModelResource))]
        public string LoginName { get; set; }
        
        
    }
}