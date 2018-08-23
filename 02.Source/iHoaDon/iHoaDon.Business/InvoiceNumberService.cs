using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    public class InvoiceNumberService : Service
    {
        private readonly IRepository<InvoiceNumber> _invoiceNumber;
        public InvoiceNumberService(IUnitOfWork context)
            : base(context)
        {
            _invoiceNumber = Context.GetRepository<InvoiceNumber>();
        }

        public InvoiceNumber GetById(int id)
        {
            return _invoiceNumber.One(InvoiceNumberQuery.WithById(id));
        }

        public IEnumerable<InvoiceNumber> GetByReleaseIdAnduseStatus(int releaseId, int useStatus)
        {
            var spec = InvoiceNumberQuery.WithAll();
            spec = spec.And(InvoiceNumberQuery.WithByReleaseId(releaseId));
            spec = spec.And(InvoiceNumberQuery.WithByUseStatus(useStatus));
            return _invoiceNumber.Find(spec);
        }

        public InvoiceNumber GetByInvoiceNumber(int invoiceNumber)
        {
            return _invoiceNumber.One(InvoiceNumberQuery.WithByInvoiceNumber(invoiceNumber));
        }
        public InvoiceNumber GetByAccountId(int accountId)
        {
            return _invoiceNumber.One(InvoiceNumberQuery.WithByAccountId(accountId));
        }
        public IEnumerable<InvoiceNumber> GelAll()
        {
            return _invoiceNumber.Find();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNumber"></param>
        public int CreateInvoiceNumbers(InvoiceNumber invoiceNumber)
        {
            _invoiceNumber.Create(invoiceNumber);
            Context.SaveChanges();
            return invoiceNumber.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNumber"></param>
        public int UpdateInvoiceNumbers(InvoiceNumber invoiceNumber)
        {
            _invoiceNumber.Update(invoiceNumber);
            return Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public int DeleteInvoiceNumbers(InvoiceNumber invoiceNumber)
        {
            _invoiceNumber.Delete(invoiceNumber);
            return Context.SaveChanges();
        }
    }
}
