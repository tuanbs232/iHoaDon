using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class InvoiceNumberQuery
    {

        public static Expression<Func<InvoiceNumber, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<InvoiceNumber, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
        public static Expression<Func<InvoiceNumber, bool>> WithByInvoiceNumber(int invoiceNumber)
        {
            return al => al.InvoicesNumber == invoiceNumber;
        }

        public static Expression<Func<InvoiceNumber, bool>> WithByAccountId(int accountId)
        {
            return al => al.AccountId == accountId;
        }
        public static Expression<Func<InvoiceNumber, bool>> WithByUseStatus(int useStatus)
        {
            return al => al.UseStatus == useStatus;
        }

        public static Expression<Func<InvoiceNumber, bool>> WithByReleaseId(int releaseId)
        {
            return al => al.ReleaseId == releaseId;
        }
    }
}
