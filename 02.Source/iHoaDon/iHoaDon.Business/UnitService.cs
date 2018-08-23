using iHoaDon.Entities;
using iHoaDon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace iHoaDon.Business
{
    public class UnitService : Service
    {
        private readonly IRepository<Unit> _unit;
        public UnitService(IUnitOfWork context)
            : base(context)
        {
            _unit = Context.GetRepository<Unit>();
        }

        public Unit GetById(int id)
        {
            return _unit.One(UnitQuerry.WithById(id));
        }
        public IEnumerable<Unit> GelAll()
        {
            return _unit.Find();
        }
        public IEnumerable<Unit> GetAllUnit(out int totalRecords,
          int currentPage = 1,
          int pageSize = 25,
          string sortBy = "Id",
          bool descending = true,
          string name = null)
        {
            var spec = UnitQuerry.WithAll();
            totalRecords = _unit.Count(spec);
            var sort = Context.Filters.Sort<Unit, int>(ti => ti.Id, true);
            switch (sortBy)
            {
                case "Id":
                    sort = Context.Filters.Sort<Unit, int>(ti => ti.Id, descending);
                    break;
                default:
                    break;
            }
            var pager = Context.Filters.Page<Unit>(currentPage, pageSize);
            return _unit.Find(spec, sort, pager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public int CreateCustomers(Unit unit)
        {
            _unit.Create(unit);
            Context.SaveChanges();
            return unit.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public int UpdateUnits(Unit unit)
        {
            _unit.Update(unit);
            return Context.SaveChanges();
        }

        public void Create(Unit unit)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (unit.Id == 0)
                        _unit.Create(unit);
                    else
                        _unit.Update(unit);
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
