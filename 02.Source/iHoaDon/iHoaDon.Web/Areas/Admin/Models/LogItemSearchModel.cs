using iHoaDon.Resources;
using iHoaDon.Resources.Admin;

namespace iHoaDon.Web.Areas.Admin.Models
{
    public class LogItemSearchModel
    {
        public int Id { get; set; }

        public string LoginName { get; set; }

        public string LoginIP { get; set; }

        public string LoginTime { get; set; }

        [VietnameseDateTime(ErrorMessageResourceName = "FromDateIsNotValid", ErrorMessageResourceType = typeof(LogItemSearchModelResource))]
        public string FromDate { get; set; }
         [VietnameseDateTime(ErrorMessageResourceName = "ToDateIsNotValid", ErrorMessageResourceType = typeof(LogItemSearchModelResource))]
        public string ToDate { get; set; }

        public bool? Status { get; set; }
    }
}