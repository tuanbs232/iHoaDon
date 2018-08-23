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
    public class ListReleaseInvoiceService : Service
    {

        private readonly IRepository<ListReleaseInvoice> _listReleaseInvoice;
        public ListReleaseInvoiceService(IUnitOfWork context)
            : base(context)
        {
            _listReleaseInvoice = Context.GetRepository<ListReleaseInvoice>();
        }

        public ListReleaseInvoice GetById(int id)
        {
            return _listReleaseInvoice.One(ListReleaseInvoiceQuery.WithById(id));
        }


        public IEnumerable<ListReleaseInvoice> GetByTemplateId(string tempCode, int accountId = -1)
        {
            var spec = ListReleaseInvoiceQuery.WithByTemplateid(tempCode);
            spec = accountId != -1 ? spec.And(ListReleaseInvoiceQuery.WithByAccountId(accountId)) : spec;


            return _listReleaseInvoice.Find(spec);
        }

        public IEnumerable<ListReleaseInvoice> GetByNo(string no)
        {
            var spec = ListReleaseInvoiceQuery.WithByNo(no);

            return _listReleaseInvoice.Find(spec);
        }

        public IEnumerable<ListReleaseInvoice> GelAll()
        {
            return _listReleaseInvoice.Find();
        }


        public IEnumerable<ListReleaseInvoice> GetAllListReleaseInvoice(out int totalRecords,
                                                                    int currentPage = 1,
                                                                    int pageSize = 25,
                                                                    string sortBy = "Id",
                                                                    bool descending = true)
        {
            var spec = ListReleaseInvoiceQuery.WithAll();

            totalRecords = _listReleaseInvoice.Count(spec);
            var sort = Context.Filters.Sort<ListReleaseInvoice, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<ListReleaseInvoice, int>(ti => ti.Id, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<ListReleaseInvoice>(currentPage, pageSize);
            return _listReleaseInvoice.Find(spec, sort, pager);
        }
        public IEnumerable<ListReleaseInvoice> GetByAcc(int acc)
        {
            var spec = ListReleaseInvoiceQuery.WithByAccountId(acc);
            return _listReleaseInvoice.Find(spec);
        }
        public IEnumerable<ListReleaseInvoice> GetByAccTemp(int acc, string temp)
        {
            var spec = ListReleaseInvoiceQuery.WithByAccountId(acc);
            spec = spec.And(ListReleaseInvoiceQuery.WithByTemplateCode(temp));
            return _listReleaseInvoice.Find(spec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listReleaseInvoice"></param>
        public int CreateListReleaseInvoices(ListReleaseInvoice listReleaseInvoice)
        {
            _listReleaseInvoice.Create(listReleaseInvoice);
            Context.SaveChanges();
            return listReleaseInvoice.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listReleaseInvoice"></param>
        public int UpdateListReleaseInvoices(ListReleaseInvoice listReleaseInvoice)
        {
            _listReleaseInvoice.Update(listReleaseInvoice);
            return Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listReleaseInvoice"></param>
        public int CancelListReleaseInvoices(ListReleaseInvoice listReleaseInvoice)
        {
            _listReleaseInvoice.Delete(listReleaseInvoice);
            return Context.SaveChanges();
        }
    }
}
