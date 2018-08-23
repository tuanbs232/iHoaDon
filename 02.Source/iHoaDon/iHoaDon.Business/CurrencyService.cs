using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    public class CurrencyService : Service
    {
        private readonly IRepository<Currency> _currency;
        public CurrencyService(IUnitOfWork context)
            : base(context)
        {
            _currency = Context.GetRepository<Currency>();
        }

        public Currency GetById(int id)
        {
            return _currency.One(CurrencyQuery.WithById(id));
        }
        public IEnumerable<Currency> GetByCode(string code)
        {
            return _currency.Find(CurrencyQuery.WithByCode(code));
        }
        
        public Currency GetByAccountId(int accountId)
        {
            return _currency.One(CurrencyQuery.WithByAccountId(accountId));
        }
        public IEnumerable<Currency> GelAll()
        {
            return _currency.Find();
        }

        public IEnumerable<Currency> GelByAccountId(int accountId)
        {
            return _currency.Find(CurrencyQuery.WithByAccountId(accountId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        public int CreateCurrencys(Currency currency)
        {
            _currency.Create(currency);
            Context.SaveChanges();
            return currency.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        public int UpdateCurrencys(Currency currency)
        {
            _currency.Update(currency);
            return Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public int DeleteCurrencys(Currency currency)
        {
            _currency.Delete(currency);
            return Context.SaveChanges();
        }
    }
}
