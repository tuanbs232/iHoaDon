using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateInvoiceService : Service
    {
        private readonly IRepository<TemplateInvoice> _templateInvoice;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TemplateInvoiceService(IUnitOfWork context)
            : base(context)
        {
            _templateInvoice = Context.GetRepository<TemplateInvoice>();
        }
        public TemplateInvoice GetById(int id)
        {
            return _templateInvoice.One(TemplateInvoiceQuery.WithById(id));
        }

        public TemplateInvoice GetByTemplateName(string templateName)
        {
            return _templateInvoice.One(TemplateInvoiceQuery.WithByTemplateName(templateName));
        }

        public IEnumerable<TemplateInvoice> GetAll()
        {
            return _templateInvoice.Find();
        }

        public TemplateInvoice GetByTemplateCode(string templateCode)
        {
            return _templateInvoice.One(TemplateInvoiceQuery.WithByTemplateCode(templateCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateInvoice"></param>
        public int CreateListReleaseInvoices(TemplateInvoice templateInvoice)
        {
            _templateInvoice.Create(templateInvoice);
            Context.SaveChanges();
            return templateInvoice.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateInvoice"></param>
        public int UpdateListReleaseInvoices(TemplateInvoice templateInvoice)
        {
            _templateInvoice.Update(templateInvoice);
            return Context.SaveChanges();
        }
    }
}
