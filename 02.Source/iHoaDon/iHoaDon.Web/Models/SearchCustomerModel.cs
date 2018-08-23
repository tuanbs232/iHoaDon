using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class SearchCustomerModel
    {

        [LocalizedDisplayName("CustomerName", NameResourceType = typeof(SearchCategoryModelResource))]
        public string CustomerName { get; set; }

        [LocalizedDisplayName("CompanyCode", NameResourceType = typeof(SearchCategoryModelResource))]
        public string CompanyCode { get; set; }

       
    }
}