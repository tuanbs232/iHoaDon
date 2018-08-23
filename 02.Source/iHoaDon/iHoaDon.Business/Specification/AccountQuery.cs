using iHoaDon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace iHoaDon.Business
{
    public class AccountQuery
    {
        public static Expression<Func<Account, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Account, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
        public static Expression<Func<Account, bool>> WithByLoginName(string loginName)
        {
            return al => al.CompanyCode.Contains(loginName);
        }
        public static Expression<Func<Account, bool>> WithNotById(int id)
        {
            return al => al.Id != id;
        }
        public static Expression<Func<Account, bool>> WithLoginName(string loginName)
        {
            return u => u.CompanyCode.Equals(loginName);
        }

    }
}
