using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business.Specification
{
    public class InvoiceQuery
    {
        public static Expression<Func<Invoice, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Invoice, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }

        public static Expression<Func<Invoice, bool>> WithByAccountId(int accountId)
        {
            return u => u.AccountId == accountId;
        }
        public static Expression<Func<Invoice, bool>> WithByAdjustmentType(string adjustmentType)
        {
            return u => u.AdjustmentType.Equals(adjustmentType);
        }

        public static Expression<Func<Invoice, bool>> WithToDate(DateTime? toDate)
        {
            return ti => ti.InvoiceIssuedDate <= toDate;
        }

        public static Expression<Func<Invoice, bool>> WithFromDateLastChanged(DateTime? fromDate)
        {
            return ti => ti.InvoiceIssuedDate >= fromDate;
        }

    }
}
