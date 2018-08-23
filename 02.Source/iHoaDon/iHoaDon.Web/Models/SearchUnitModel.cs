using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iHoaDon.Entities;
using iHoaDon.Resources.Client;

namespace iHoaDon.Web
{
    public class SearchUnitModel
    {
        [LocalizedDisplayName("Name", NameResourceType = typeof(SearchUnitModelResource))]
        public string Name { get; set; }
    }
}