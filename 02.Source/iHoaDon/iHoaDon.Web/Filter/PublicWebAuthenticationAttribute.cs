using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDemoSigning.Filter
{
    public class PublicWebAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string url = filterContext.HttpContext.Request.Url.AbsolutePath;

            Boolean isAuthorized = HttpContext.Current.Request.IsAuthenticated;

            if (isAuthorized)
            {
                if (url.Equals("/") || url.StartsWith("/Home/Index"))
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }
            }
            else
            {
                if (url.Equals("/") || url.StartsWith("/Home/Index"))
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
            }
        }
    }
}