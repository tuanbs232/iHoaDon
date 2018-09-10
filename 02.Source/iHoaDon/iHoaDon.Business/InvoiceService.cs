using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Transactions;
using iHoaDon.Business.Specification;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;
using Transaction = iHoaDon.Entities.Transaction;

namespace iHoaDon.Business
{
    public class InvoiceService : Service
    {
        private readonly IRepository<Invoice> _invoice;
        private readonly IRepository<InvoiceDetail> _invoiceDetail;
        private readonly IRepository<Transaction> _transaction;
        public InvoiceService(IUnitOfWork context)
            : base(context)
        {
            _invoice = Context.GetRepository<Invoice>();
            _invoiceDetail = Context.GetRepository<InvoiceDetail>();
            _transaction = Context.GetRepository<Transaction>();
        }

        public Invoice GetById(int id)
        {
            return _invoice.One(InvoiceQuery.WithById(id));
        }

       
        public IEnumerable<Invoice> GelAll()
        {
            return _invoice.Find();
        }

        public IEnumerable<Invoice> GetAllInvoice(out int totalRecords,
                                                                    int currentPage = 1,
                                                                    int pageSize = 25,
                                                                    string sortBy = "Id",
                                                                    bool descending = true)
        {
            var spec = InvoiceQuery.WithAll();

            totalRecords = _invoice.Count(spec);
            var sort = Context.Filters.Sort<Invoice, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<Invoice, int>(ti => ti.Id, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<Invoice>(currentPage, pageSize);
            return _invoice.Find(spec, sort, pager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Invoice"></param>
        public int CreateInvoices(Invoice invoice)
        {
            _invoice.Create(invoice);
            Context.SaveChanges();
            return invoice.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Invoice"></param>
        public int UpdateInvoices(Invoice invoice)
        {
            _invoice.Update(invoice);
            return Context.SaveChanges();
        }

        public int Create(Invoice invoice,Transaction transaction)
        {
           using (var scope = new TransactionScope())
           {
               try
               {
                   var dateNow = DateTime.Now;
                   invoice.InvoiceIssuedDate = dateNow;
                   _invoice.Create(invoice);
                   Context.SaveChanges();

                    transaction.InvoiceID = invoice.Id;
                   transaction.CreateDate = dateNow;
                   transaction.DateModify = dateNow;
                   
                   _transaction.Create(transaction);
                   Context.SaveChanges();

                   //_invoiceDetail.Create(invoiceDetail);
                   //Context.SaveChanges();
                   scope.Complete();
                   return transaction.Id;

               }catch(Exception ex)
               {
                   scope.Dispose();
                   throw ex;
               }
           }
        }
    }
}
