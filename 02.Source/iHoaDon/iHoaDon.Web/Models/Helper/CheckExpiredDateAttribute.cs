using System;
using System.Web.Mvc;
using iHoaDon.Business;

namespace iHoaDon.Web
{
    public class CheckExpiredDateAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>.</param><exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            
            if (!filterContext.HttpContext.User.IsInRole(Entities.Roles.Admin)
                && !filterContext.HttpContext.User.IsInRole(Entities.Roles.AdminAccountManager)
                && !filterContext.HttpContext.User.IsInRole(Entities.Roles.AdminTechSupport))
            {
                var expireDate = filterContext.HttpContext.User.GetExpireDate();
                if (expireDate.HasValue)
                {
                    if (DateTime.Now.CompareTo(expireDate.Value) > 0)
                    {
                        filterContext.Result = new RedirectResult("~/Error/ExpiredDate");
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Account/LogOff");
                }
            }
        }
    }
}