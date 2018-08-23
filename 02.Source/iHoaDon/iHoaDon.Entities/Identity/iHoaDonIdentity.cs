using System;
using System.Security.Principal;

namespace iHoaDon.Entities
{
    /// <summary>
    /// A custom IIdentity that hold information related to Tvan's operation
    /// </summary>
    public class iHoaDonIdentity : IIdentity
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="iHoaDonIdentity"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The account id.</param>
        /// <param name="roleCode">The role code.</param>
        /// ///
        public iHoaDonIdentity(string name, int id, byte roleCode, long permission, bool isMasterAccount, DateTime? expireDate)
        {
            Name = name;
            Id = id;
            RoleCode = roleCode;
            Role = Roles.GetRoleName(roleCode);
            Permission = permission;
            IsMasterAccount = isMasterAccount;
            ExpireDate = expireDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="iHoaDonIdentity"/> class.
        /// NOTE: this constructor is used to deserialize a cookie's decrypted UserDataString
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="data">The data string.</param>
        public iHoaDonIdentity(string name, string data)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (String.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            Name = name;
            var parts = data.Split(new[] { '|' });
            if (parts.Length > 5)
            {
                throw new ArgumentException("data");
            }

            Id = Int32.Parse(parts[0]);
            RoleCode = Byte.Parse(parts[1]);
            Role = Roles.GetRoleName(RoleCode);
            Permission = long.Parse(parts[2]);
            IsMasterAccount = bool.Parse(parts[3]);
            DateTime expire;
            if (DateTime.TryParse(parts[4], out expire))
            {
                ExpireDate = expire;
            }

        }
        #endregion

        #region Implementation of IIdentity
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the user on whose behalf the code is running.</returns>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <value></value>
        /// <returns>The type of authentication used to identify the user.</returns>
        public string AuthenticationType { get { return "Custom"; } }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <value></value>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated { get { return true; } }
        #endregion

        #region Business information
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; private set; }//0


        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; private set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public byte RoleCode { get; private set; }//2
        public long Permission { get; private set; }//3

        /// <summary>
        /// Gets or sets is master account.
        /// </summary>
        /// <value>The is master account.</value>
        public bool IsMasterAccount { get; private set; }//4

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public DateTime? ExpireDate { get; private set; }//4

        #endregion

        /// <summary>
        /// Serialize this identity to a delimited string (for cookie storage).
        /// </summary>
        /// <returns></returns>
        public string ToCookieString()
        {
            return String.Join("|",
                               Id, //0
                               RoleCode,
                               Permission, //3
                               IsMasterAccount, //4
                               ExpireDate);
        }
    }
}