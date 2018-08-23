using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace iHoaDon.Web.Helper
{
    public static class SortLinkHelper
    {
        public static MvcHtmlString SortLink(this AjaxHelper helper,
                                                string columnName,
                                                string text,
                                                string currentSortBy,
                                                bool sortDescending,
                                                string actionName,
                                                object valuesDictionary,
                                                AjaxOptions ajaxOptions,
                                                string cssSortAsc,
                                                string cssSortDesc)
        {
            var viewContext = helper.ViewContext;
            var isDescending = string.CompareOrdinal(currentSortBy, columnName) == 0 && !sortDescending;
            var action = valuesDictionary != null
                             ? new RouteValueDictionary(valuesDictionary) { { "action", actionName },
                                                                            {"sortBy", columnName},
                                                                            {"sortDesc", isDescending}
                                                                          }
                             : new RouteValueDictionary
                                   { { "action", actionName },
                                                            {"sortBy", columnName},
                                                            {"sortDesc", isDescending}
                                                          };
            var classSort = "";
            if (string.CompareOrdinal(currentSortBy, columnName) == 0)
            {
                classSort = sortDescending ? cssSortDesc : cssSortAsc;
            }

            return new MvcHtmlString(GeneratePageLink(viewContext, 
                                                        text, 
                                                        action, 
                                                        ajaxOptions, 
                                                        classSort));
        }

        private static string GeneratePageLink(ViewContext viewContext,
                                                string linkText,
                                                RouteValueDictionary action,
                                                AjaxOptions ajaxOptions,
                                                string cssClass)
        {
            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, action);

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
    }
}