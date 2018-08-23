using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class ListReleaseInvoiceQuery
    {
        public static Expression<Func<ListReleaseInvoice, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<ListReleaseInvoice, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }

        public static Expression<Func<ListReleaseInvoice, bool>> WithByAccountId(int id)
        {
            return al => al.AccountId == id;
        }
        public static Expression<Func<ListReleaseInvoice, bool>> WithByTemplateid(string tempCode)
        {
            return al => al.TemplateCode.Equals(tempCode);
        }

        public static Expression<Func<ListReleaseInvoice, bool>> WithByNo(string no)
        {
            return al => al.No.ToLower().Equals(no.ToLower());
        }
        public static Expression<Func<ListReleaseInvoice, bool>> WithByTemplateCode(string templateCode)
        {
            return al => al.TemplateCode.ToLower().Equals(templateCode.ToLower());
        }
    }
}
