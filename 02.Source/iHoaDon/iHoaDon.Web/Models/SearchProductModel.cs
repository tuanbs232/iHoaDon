using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class SearchProductModel
    {

        [LocalizedDisplayName("ProductCode", NameResourceType = typeof(SearchProductModelResource))]
        public string ProductCode { get; set; }

        [LocalizedDisplayName("ProductName", NameResourceType = typeof(SearchProductModelResource))]
        public string ProductName { get; set; }
        
    }
}