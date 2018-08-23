using iHoaDon.Entities;

namespace iHoaDon.Web.Areas.Admin.Models
{
    public class LogActionSearchModel
    {
        public int Id { get; set; }

        public string LoginName { get; set; }

        public string ActionContent { get; set; }

        public string DataBeforeChange { get; set; }

        public string DataAfterChange { get; set; }
        [VietnameseDateTime]
        public string FromDate { get; set; }

        [VietnameseDateTime]
        public string ToDate { get; set; }

        public string ActionTime { get; set; }

        public LogActionType? ActionType { get; set; }
    }
}