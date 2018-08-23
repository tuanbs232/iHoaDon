using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class SearchInvoiceModel
    {
        [LocalizedDisplayName("InvoiceType", NameResourceType = typeof(SearchInvoiceModelResource))]
        public string InvoiceType { get; set; }

        [LocalizedDisplayName("FromDate", NameResourceType = typeof(SearchInvoiceModelResource))]
        public string FromDate { get; set; }

        [LocalizedDisplayName("ToDate", NameResourceType = typeof(SearchInvoiceModelResource))]
        public string ToDate { get; set; }


        [LocalizedDisplayName("AdjustmentType", NameResourceType = typeof(SearchInvoiceModelResource))]
        public string AdjustmentType { get; set; }

        public IEnumerable<SelectListItem> ListNo { get; set; }
    }
}