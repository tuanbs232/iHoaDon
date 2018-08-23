using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;


namespace iHoaDon.Business
{
    public class TemplateInvoiceQuery
    {

        public static Expression<Func<TemplateInvoice, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<TemplateInvoice, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
        public static Expression<Func<TemplateInvoice, bool>> WithByTemplateName(string templateName)
        {
            return al => al.TemplateName.Contains(templateName);
        }
        public static Expression<Func<TemplateInvoice, bool>> WithByTemplateCode(string templateCode)
        {
            return al => al.TemplateCode.ToLower().Equals(templateCode.ToLower());
        }
    }
}
