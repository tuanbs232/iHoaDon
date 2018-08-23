using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class ProductQuery
    {

        public static Expression<Func<Product, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Product, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
        public static Expression<Func<Product, bool>> WithByCode(string code)
        {
            return al => al.ProductCode.Equals(code);
        }

        public static Expression<Func<Product, bool>> WithByAccountId(int accountId)
        {
            return u => u.AccountId == accountId;
        }
        public static Expression<Func<Product, bool>> WithByProName(string productName)
        {
            return u => u.ProductName.Contains(productName);
        }
    }
}
