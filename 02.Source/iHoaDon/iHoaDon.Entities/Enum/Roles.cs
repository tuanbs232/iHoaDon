using System;
using System.Collections.Generic;

namespace iHoaDon.Entities
{
    /// <summary>
    /// The roles
    /// </summary>
    public static class Roles
    {
        /// <summary>
        /// Role names
        /// </summary>
        public const string Admin = "admin",
                            AdminTechSupport = "adminTechSupport",
                            AdminAccountManager = "adminAccountManager",
                            Personal = "personal",
                            Business = "business",
                            BusinessUser = "businessUser",
                            Service = "service",
                            ServiceUser = "serviceUser",
                            ServiceCheckUser = "serviceCheckUser";
                            
        private const string Comma = ",";

        /// <summary>
        /// 
        /// </summary>
        public const string NonAdmin = Personal + Comma + Business + Comma + Service + Comma + BusinessUser + Comma + ServiceUser;
        /// <summary>
        /// 
        /// </summary>
        public static readonly string SomeAdmin = string.Join(",", new[] { Admin, AdminTechSupport, AdminAccountManager, ServiceCheckUser }),

                                        All = string.Join(",", new[] { Admin, AdminTechSupport, AdminAccountManager, Service, ServiceUser, Business, BusinessUser, Personal, ServiceCheckUser });

        /// <summary>
        /// Role codes (used primarily for cookie creation)
        /// </summary>
        public const int PersonalCode = 0,
                            BusinessUserCode = 1,
                            BusinessCode = 2,
                            ServiceCode = 3,
                            ServiceUserCode = 4,
                            AdminTechSupportCode = 5,
                            AdminAccountManagerCode = 6,
                            ServiceCheckUserCode=8,
                            AdminCode = 7;


        /// <summary>
        /// 
        /// </summary>
        public static readonly string[] AllRoles = new[] { Personal, BusinessUser, Business, Service, ServiceUser, AdminTechSupport, AdminAccountManager, Admin, ServiceCheckUser };

        /// <summary>
        /// Gets the role code.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public static int GetRoleCode(string role)
        {
            var result = Array.IndexOf(AllRoles, role);
            if (result == -1)
            {
                throw new Exception("Invalid role name");
            }
            return result;
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static string GetRoleName(int code)
        {
            if (code < 0 || code > AllRoles.Length)
            {
                throw new Exception("Invalid code index");
            }
            return AllRoles[code];
        }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<int, string> AllRoleNames = new Dictionary<int, string>
        {
            {AdminCode,                 "Quản trị hệ thống"},
            {AdminTechSupportCode,      "Hỗ trợ kỹ thuật"},
            {AdminAccountManagerCode,   "Quản trị tài khoản"},
            {PersonalCode,              "Cá nhân"},
            {BusinessCode,              "Doanh nghiệp"},
            {BusinessUserCode,          "Tài khoản con của doanh nghiệp"},
            {ServiceCode,               "Đại lý"},
            {ServiceUserCode,           "Tài khoản con của đại lý"},
            {ServiceCheckUserCode,           "Tài khoản tìm kiếm khách hàng"}
        };
    }
}
