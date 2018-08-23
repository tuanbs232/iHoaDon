using System.Web.Mvc;
using iHoaDon.Business;
using iHoaDon.Entities;
using System;

namespace iHoaDon.Web
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>.</param><exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.Cookies["checkLogin"] != null)
                {
                    filterContext.HttpContext.Response.Cookies["checkLogin"].Value = string.Empty;
                }
            }
            if ((filterContext.Result is HttpUnauthorizedResult) && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Error/AccessDenied");
            }
        }
    }
}