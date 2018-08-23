using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class CurrencyQuery
    {

        public static Expression<Func<Currency, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Currency, bool>> WithByCode(string code)
        {
            return al => al.CurrencyCode.Equals(code);
        }
        public static Expression<Func<Currency, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }

        public static Expression<Func<Currency, bool>> WithByAccountId(int accountId)
        {
            return al => al.AccountId == accountId;
        }

    }
}
