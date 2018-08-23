using iHoaDon.Entities;

namespace iHoaDon.Business
{
    /// <summary>
    /// Extension for Permission
    /// </summary>
    public static class PermissionsExtension
    {
        /// <summary>
        /// Does the permission set constitute an admin?
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns>
        /// 	<c>true</c> if the specified @this is admin; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAdmin(this Permissions @this)
        {
            return @this >= Permissions.ManageForms;
        }

        /// <summary>
        /// Does the permission set constitute an user?
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns>
        /// 	<c>true</c> if the specified @this is user; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUser(this Permissions @this)
        {
            return @this < Permissions.ManageForms;
        }
    }
}