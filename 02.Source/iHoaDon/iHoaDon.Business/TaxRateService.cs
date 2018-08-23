using iHoaDon.Entities.Entities;
using iHoaDon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace iHoaDon.Business
{
    class TaxRateService : Service
    {
        private readonly IRepository<TaxRate> _taxRate;
        public TaxRateService(IUnitOfWork context)
            : base(context)
        {
            _taxRate = Context.GetRepository<TaxRate>();
        }

        public TaxRate GetById(int id)
        {
            return _taxRate.One(UnitQuerry.WithById(id));
        }
        public IEnumerable<TaxRate> GelAll()
        {
            return _taxRate.Find();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public int CreateCustomers(TaxRate unit)
        {
            _taxRate.Create(unit);
            Context.SaveChanges();
            return unit.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public int UpdateUnits(TaxRate unit)
        {
            _taxRate.Update(unit);
            return Context.SaveChanges();
        }

        public void Create(TaxRate unit)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (unit.Id == 0)
                        _taxRate.Create(unit);
                    else
                        _taxRate.Update(unit);
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
