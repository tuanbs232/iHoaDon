using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    public class CustomerService : Service
    {
        // logger for this class
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // 
        private readonly IRepository<Customer> _customer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CustomerService(IUnitOfWork context)
            : base(context)
        {
            _customer = Context.GetRepository<Customer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetById(int id)
        {
            return _customer.One(CustomerQuery.WithById(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public Customer GetByCompanyCode(string companyCode)
        {
            return _customer.One(CustomerQuery.WithByCompanyCode(companyCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GelAll()
        {
            return _customer.Find();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetByCompanyName(string name)
        {
            var result = _customer.Find(CustomerQuery.WithCompanyName(name));
            if (result == null)
            {
                throw new Exception("No records");
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="descending"></param>
        /// <param name="customerName"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomer(out int totalRecords,
            int currentPage = 1,
            int pageSize = 25,
            string sortBy = "Id",
            bool descending = true,
            string customerName = null, 
            string companyCode = null)
        {
            var spec = CustomerQuery.WithAll();
            totalRecords = _customer.Count(spec);
            var sort = Context.Filters.Sort<Customer, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<Customer, int>(ti => ti.Id, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<Customer>(currentPage, pageSize);
            return _customer.Find(spec, sort, pager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public int CreateCustomers(Customer customer)
        {
            _customer.Create(customer);
            Context.SaveChanges();
            return customer.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public int UpdateCustomers(Customer customer)
        {
            _customer.Update(customer);
            return Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public bool Create(Customer customer)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (customer.Id == 0)
                        _customer.Create(customer);
                    else
                        _customer.Update(customer);
                    Context.SaveChanges();
                    scope.Complete();

                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    _log.Error("Create Customer error. " + ex.Message, ex);

                    return false;
                }
            }
        }
    }
}
