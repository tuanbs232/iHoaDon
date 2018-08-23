namespace iHoaDon.Web.Models
{
    public class SortAndPageModel
    {
        public SortAndPageModel()
        {
            PageSize = 25;
        }

        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecordCount { get; set; }
    }
}