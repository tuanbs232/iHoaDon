using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class CustomerInvoiceQuery
    {
        public static Expression<Func<CustomerInvoice, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<CustomerInvoice, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }

        public static Expression<Func<CustomerInvoice, bool>> WithByCompanyCode(string companyCode)
        {
            return u => u.CompanyCode.Equals(companyCode);
        }

        public static Expression<Func<CustomerInvoice, bool>> WithByCompanyName(string companyName)
        {
            return u => u.CompanyName.Contains(companyName);
        }

        public static Expression<Func<Product, bool>> WithByAccountId(int accountId)
        {
            return u => u.AccountId == accountId;
        }
    }
}
