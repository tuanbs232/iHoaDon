using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class UnitModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessageResourceName = "NameMaxLength", ErrorMessageResourceType = typeof(UnitModelResource))]
        [LocalizedDisplayName("Name", NameResourceType = typeof(UnitModelResource))]
        [Required(ErrorMessageResourceName = "NameRequied", ErrorMessageResourceType = typeof(UnitModelResource))]
        public string Name { get; set; }

    }
}