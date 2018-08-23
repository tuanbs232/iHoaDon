using System;
using System.Security.Principal;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    /// <summary>
    /// Extensions for user
    /// </summary>
    public static class UserInfoExtension
    {
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static string GetUserName(this IPrincipal user)
        {
            var result = "";
            if (user != null)
            {
                var identity = user.Identity;
                if (identity.IsAuthenticated)
                {
                    result = identity.Name;
                }
            }
            return result;
        }

        

        /// <summary>
        /// Gets the account id.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static int GetAccountId(this IPrincipal user)
        {
            var result = -1;
            var principal = user as iHoaDonPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as iHoaDonIdentity;
                if (identity != null)
                {
                    result = identity.Id;
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the expire date.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static DateTime? GetExpireDate(this IPrincipal user)
        {
            DateTime? result = null;
            var principal = user as iHoaDonPrincipal;
            if (principal != null)
            {
                var identity = principal.Identity as iHoaDonIdentity;
                if (identity != null)
                {
                    result = identity.ExpireDate;
                }
            }
            return result;
        }

    }
}