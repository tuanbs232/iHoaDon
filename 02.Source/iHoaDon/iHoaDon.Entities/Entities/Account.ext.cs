using iHoaDon.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iHoaDon.Entities
{
    /// <summary>
    /// Extensions
    /// </summary>
    partial class Account
    {
        /// <summary>
        /// Gets or sets the role name (currently fixed to only admin and user).
        /// </summary>
        /// <value>The name of the role.</value>
        [Required]
        [StringLength(64)]
        public string RoleName
        {
            get { return Roles.GetRoleName(RoleCode); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a master account.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is master account; otherwise, <c>false</c>.
        /// </value>
        [NotMapped]
        public bool IsMasterAccount { get { return !MasterAccountId.HasValue; } }

        /// <summary>
        /// Gets a value indicating whether this instance is a sub account.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is sub account; otherwise, <c>false</c>.
        /// </value>
        [NotMapped]
        public bool IsSubAccount { get { return MasterAccountId.HasValue; } }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        [NotMapped]
        public Permissions Permissions
        {
            get { return (Permissions)PermissionFlags; }
            set { PermissionFlags = (long)value; }
        }
    }
}