using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class CustomerQuery
    {
        public static Expression<Func<Customer, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Customer, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }

        public static Expression<Func<Customer, bool>> WithByCompanyCode(string companyCode)
        {
            return u => u.CompanyCode.Equals(companyCode);
        }

        public static Expression<Func<Customer, bool>> WithByCompanyName(string companyName)
        {
            return u => u.CompanyName.Contains(companyName);
        }

        public static Expression<Func<Product, bool>> WithByAccountId(int accountId)
        {
            return u => u.AccountId == accountId;
        }
        public static Expression<Func<Customer, bool>> WithCompanyName(string companyName)
        {
            return al => al.CompanyName.Contains(companyName);
        }
    }
}
