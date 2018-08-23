using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    public class CustomerInvoiceService : Service
    {
        private readonly IRepository<CustomerInvoice> _customerInvoice;
        public CustomerInvoiceService(IUnitOfWork context)
            : base(context)
        {
            _customerInvoice = Context.GetRepository<CustomerInvoice>();
        }

        public CustomerInvoice GetById(int id)
        {
            return _customerInvoice.One(CustomerInvoiceQuery.WithById(id));
        }

        public CustomerInvoice GetByCompanyCode(string companyCode)
        {
            return _customerInvoice.One(CustomerInvoiceQuery.WithByCompanyCode(companyCode));
        }

        public IEnumerable<CustomerInvoice> GelAll()
        {
            return _customerInvoice.Find();
        }

        public IEnumerable<CustomerInvoice> GetAllCustomerInvoice(out int totalRecords,
            int currentPage = 1,
            int pageSize = 25,
            string sortBy = "Id",
            bool descending = true,
            string customerInvoiceName = null, 
            string companyCode = null)
        {
            var spec = CustomerInvoiceQuery.WithAll();
            totalRecords = _customerInvoice.Count(spec);
            var sort = Context.Filters.Sort<CustomerInvoice, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<CustomerInvoice, int>(ti => ti.Id, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<CustomerInvoice>(currentPage, pageSize);
            return _customerInvoice.Find(spec, sort, pager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerInvoice"></param>
        public int CreateCustomerInvoices(CustomerInvoice customerInvoice)
        {
            _customerInvoice.Create(customerInvoice);
            Context.SaveChanges();
            return customerInvoice.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerInvoice"></param>
        public int UpdateCustomerInvoices(CustomerInvoice customerInvoice)
        {
            _customerInvoice.Update(customerInvoice);
            return Context.SaveChanges();
        }

        public void Create(CustomerInvoice customerInvoice)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (customerInvoice.Id == 0)
                        _customerInvoice.Create(customerInvoice);
                    else
                        _customerInvoice.Update(customerInvoice);
                    Context.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
    }
}
