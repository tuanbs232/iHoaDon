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
    public class CancelinvoiceModel
    {
        public int Id { get; set; }
        //Loại hóa đơn
        [LocalizedDisplayName("TemplateCode", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "TemplateCodeRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public string TemplateId { get; set; }

        //Mẫu số
        [StringLength(20, ErrorMessageResourceName = "NoMaxLength", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        [LocalizedDisplayName("No", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "NoRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public string No { get; set; }

        //Ký hiệu
        [StringLength(20, ErrorMessageResourceName = "SerialInvoiceMaxLength", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        [LocalizedDisplayName("SerialInvoice", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "SerialInvoiceRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public string SerialInvoice { get; set; }


        //Số lượng hóa đơn
        [LocalizedDisplayName("Quantity", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "QuantityRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public int Quantity { get; set; }

        //ngày bắt đầu
        [LocalizedDisplayName("StartUsingDate", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "StartUsingDateRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public string StartUsingDate { get; set; }

        //từ số 
        [LocalizedDisplayName("StartNumber", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "StartNumberRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public int StartNumber { get; set; }

        //đến số
        [LocalizedDisplayName("EndNumber", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "EndNumberRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public int EndNumber { get; set; }

        //đã sử dụng đến số
          //đến số
        [LocalizedDisplayName("ToNumber", NameResourceType = typeof(ReissueinvoiceResource))]
        [Required(ErrorMessageResourceName = "ToNumberRequied", ErrorMessageResourceType = typeof(ReissueinvoiceResource))]
        public int ToNumber { get; set; }
        

        public IEnumerable<SelectListItem> ListNo { get; set; }
    }
}