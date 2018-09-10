using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class TransactionQuery
    {

        public static Expression<Func<Transaction, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        
        public static Expression<Func<Transaction, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }

        public static Expression<Func<Transaction, bool>> WithByInvoiceId(int invoiceId)
        {
            return u => u.InvoiceID == invoiceId;
        }
    }
}
