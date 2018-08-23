using System;
using System.Security.Principal;

namespace iHoaDon.Entities
{
    /// <summary>
    /// Security principal for ASP.NET and .NET
    /// </summary>
    public class iHoaDonPrincipal : IPrincipal
    {
        private readonly iHoaDonIdentity _identity;

        #region Implementation of IPrincipal
        /// <summary>
        /// Initializes a new instance of the <see cref="iHoaDonPrincipal"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        public iHoaDonPrincipal(iHoaDonIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            _identity = identity;
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role)
        {
            return String.Equals(_identity.Role, role, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.</returns>
        public IIdentity Identity
        {
            get { return _identity; }
        }
        #endregion
    }
}