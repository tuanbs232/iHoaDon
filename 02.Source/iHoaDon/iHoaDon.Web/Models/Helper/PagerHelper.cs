using System;
using System.Web.Mvc;
using System.Text;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace iHoaDon.Web
{
    public static class PagerHelper
    {
        private static int _totalRecords, _pageCount, _pageSize, _currentPage;
        private static ViewContext _viewContext;
        private static string _cssPagerButton, _cssPagerButtonDisabled, _cssPagerButtonCurrentPage;
        private static AjaxOptions _ajaxOptions;
        private static RouteValueDictionary _action;
        private static string _timePage;

        public static MvcHtmlString Pager(this AjaxHelper helper,
                                    int pageSize,
                                    int currentPage,
                                    int totalRecords,
                                    string actionName,
                                    object valuesDictionary,
                                    AjaxOptions ajaxOptions,
                                    string cssPagerButton,
                                    string cssPagerButtonDisabled,
                                    string cssPagerButtonCurrentPage, string timePage = null)
        {
            //set value
            _totalRecords = totalRecords;
            _pageSize = pageSize;
            _currentPage = currentPage;
            _viewContext = helper.ViewContext;
            _action = valuesDictionary != null
                             ? new RouteValueDictionary(valuesDictionary) { { "action", actionName } }
                             : new RouteValueDictionary { { "action", actionName } };
            _ajaxOptions = ajaxOptions;
            _pageCount = (int)Math.Ceiling(totalRecords / (double)pageSize);
            _cssPagerButton = cssPagerButton;
            _cssPagerButtonDisabled = cssPagerButtonDisabled;
            _cssPagerButtonCurrentPage = cssPagerButtonCurrentPage;
            _timePage = timePage;
            const int adjacents = 3;
            var sb = new StringBuilder("<div style='float:left; margin-top: 5px;'>");
            GeneratePrevious(sb);
            // don't need to break it up
            if (_pageCount < (7 + adjacents * 2))
            {
                AddRange(sb, 1, _pageCount);
            }
            else
            {
                if (currentPage < (1 + adjacents * 2)) // hide just the end
                {
                    AddRange(sb, 1, 4 + adjacents * 2);
                    AddLastTwo(sb);
                }
                else if (_pageCount - (adjacents * 2) > currentPage
                            && currentPage > (adjacents * 2)) // hide on both sides
                {
                    AddFirstTwo(sb);
                    AddRange(sb, currentPage - adjacents, currentPage + adjacents);
                    AddLastTwo(sb);
                }
                else // hide just the beginning
                {
                    AddFirstTwo(sb);
                    AddRange(sb, _pageCount - (2 + (adjacents * 2)), _pageCount);
                }
            }
            GenerateNext(sb);
            sb.Append("</div>");

            //page size
            sb.Append(GeneratePageSizeDropdown(_viewContext, pageSize, _action, ajaxOptions));

            return new MvcHtmlString(sb.ToString());
        }

        private static string GeneratePageSizeDropdown(ViewContext viewContext, 
                                                        int pageSize,
                                                        RouteValueDictionary action,
                                                        AjaxOptions ajaxOptions)
        {
            var pageLink = new RouteValueDictionary(action) { { "page", 1 } };
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLink);

            if (virtualPathForArea == null)
                return null;
            var stringBuilder = new StringBuilder("<div style='float:left'><form method=\"get\"");

            if (ajaxOptions != null)
                foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
                    stringBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);

            stringBuilder.AppendFormat(" action=\"{0}\" >",
                                        virtualPathForArea.VirtualPath);

            stringBuilder.Append("&nbsp;&nbsp;&nbsp;Số kết quả/trang:&nbsp;<select name=\"pageSize\" style=\"width:60px;\" onchange=\"$(this).parents('form:first').submit()\">");
            for (var i = 10; i <= 30; i += 5)
            {
                stringBuilder.Append("<option value=\"" + i + "\"");
                if(i == pageSize)
                {
                    stringBuilder.Append(" selected=\"selected\"");
                }
                stringBuilder.Append(" >" + i + "</option>");
            }
            stringBuilder.Append("</select></form></div>");
            string pageTime = "";
            if(_timePage!=null)
                pageTime=  " - thời gian xử lý: " + _timePage + " ";
            stringBuilder.Append("<div style='float:right; margin-top:5px'>&nbsp;&nbsp;" + _totalRecords +
                                 " kết quả trong " + _pageCount + " trang" + pageTime + " </div>");

            return stringBuilder.ToString();
        }

        private static string GeneratePageLink(ViewContext viewContext,
                                                string linkText,
                                                int pageNumber,
                                                int pageSize,
                                                RouteValueDictionary action,
                                                AjaxOptions ajaxOptions,
                                                string cssClass)
        {
            var pageLink = new RouteValueDictionary(action) { { "page", pageNumber }, { "pageSize", pageSize } };
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLink);

            if (virtualPathForArea == null)
                return null;

            var stringBuilder = new StringBuilder("<a");

            if (ajaxOptions != null)
                foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
                    stringBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);

            stringBuilder.AppendFormat(" href=\"{0}\" class=\"{1}\">{2}</a>", 
                                        virtualPathForArea.VirtualPath, 
                                        cssClass, 
                                        linkText);

            return stringBuilder.ToString();
        }

        private static void GeneratePrevious(StringBuilder sb)
        {
            if (_currentPage > 1)
            {
                sb.Append(GeneratePageLink(_viewContext,
                                            "|&lt;",
                                            1,
                                            _pageSize,
                                            _action,
                                            _ajaxOptions,
                                            _cssPagerButton));
                sb.Append(GeneratePageLink(_viewContext,
                                            "&lt;&lt;",
                                            _currentPage - 1,
                                            _pageSize,
                                            _action,
                                            _ajaxOptions,
                                            _cssPagerButton));
            }
            else
            {
                sb.Append("<span class=\"" + _cssPagerButtonDisabled + "\">|&lt;</span>");
                sb.Append("<span class=\"" + _cssPagerButtonDisabled + "\">&lt;&lt;</span>");
            }
        }

        private static void GenerateNext(StringBuilder sb)
        {
            if (_currentPage < _pageCount)
            {
                sb.Append(GeneratePageLink(_viewContext,
                                            "&gt;&gt;",
                                            _currentPage + 1,
                                            _pageSize,
                                            _action,
                                            _ajaxOptions,
                                            _cssPagerButton));
                sb.Append(GeneratePageLink(_viewContext,
                                            "&gt;|",
                                            _pageCount,
                                            _pageSize,
                                            _action,
                                            _ajaxOptions,
                                            _cssPagerButton));
            }
            else
            {
                sb.Append("<span class=\"" + _cssPagerButtonDisabled + "\">&gt;&gt;</span>");
                sb.Append("<span class=\"" + _cssPagerButtonDisabled + "\">&gt;|</span>");
            }
        }

        private static void AddFirstTwo(StringBuilder sb)
        {
            AddRange(sb, 1, 2);
            sb.Append("...");
        }

        private static void AddLastTwo(StringBuilder sb)
        {
            sb.Append("...");
            AddRange(sb, _pageCount - 1, _pageCount);
        }

        private static void AddRange(StringBuilder sb, int start, int end)
        {
            for (var i = start; i <= end; i++)
            {
                if (i == _currentPage)
                {
                    sb.Append("<span class=\"" + _cssPagerButtonCurrentPage + "\">" + i + "</span>");
                }
                else
                {
                    sb.Append(GeneratePageLink(_viewContext,
                                                i.ToString(),
                                                i,
                                                _pageSize,
                                                _action,
                                                _ajaxOptions,
                                                _cssPagerButton));
                }
            }
        }
    }
}