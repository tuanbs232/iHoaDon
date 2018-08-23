using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        [StringLength(50, ErrorMessageResourceName = "ProductCodeMaxLength", ErrorMessageResourceType = typeof(ProductModelResource))]
        [LocalizedDisplayName("ProductCode", NameResourceType = typeof(ProductModelResource))]
        [Required(ErrorMessageResourceName = "ProductCodeRequied", ErrorMessageResourceType = typeof(ProductModelResource))]
        public string ProductCode { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "ProductNameMaxLength", ErrorMessageResourceType = typeof(ProductModelResource))]
        [LocalizedDisplayName("ProductName", NameResourceType = typeof(ProductModelResource))]
        [Required(ErrorMessageResourceName = "ProductNameRequied", ErrorMessageResourceType = typeof(ProductModelResource))]
        public string ProductName { get; set; }

        [LocalizedDisplayName("UnitID", NameResourceType = typeof(ProductModelResource))]
        [Required(ErrorMessageResourceName = "UnitIDRequied", ErrorMessageResourceType = typeof(ProductModelResource))]
        public int UnitID { get; set; }


        [StringLength(20, ErrorMessageResourceName = "PriceMaxLength", ErrorMessageResourceType = typeof(ProductModelResource))]
        [LocalizedDisplayName("Price", NameResourceType = typeof(ProductModelResource))]
        [Required(ErrorMessageResourceName = "PriceRequied", ErrorMessageResourceType = typeof(ProductModelResource))]
        public string Price { get; set; }

        [StringLength(200, ErrorMessageResourceName = "NoteMaxLength", ErrorMessageResourceType = typeof(ProductModelResource))]
        [LocalizedDisplayName("Note", NameResourceType = typeof(ProductModelResource))]
        [Required(ErrorMessageResourceName = "NoteRequied", ErrorMessageResourceType = typeof(ProductModelResource))]
        public string Note { get; set; }


        public IEnumerable<SelectListItem> ListUnitId { get; set; }
    }
}