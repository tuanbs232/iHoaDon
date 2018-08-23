using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    public class BanksService : Service
    {
        private readonly IRepository<Banks> _Banks;
        public BanksService(IUnitOfWork context)
            : base(context)
        {
            _Banks = Context.GetRepository<Banks>();
        }

        public Banks GetById(int id)
        {
            return _Banks.One(BanksQuery.WithById(id));
        }

        
        public Banks GetByAccountId(int accountId)
        {
            return _Banks.One(BanksQuery.WithByAccountId(accountId));
        }
        public IEnumerable<Banks> GelAll()
        {
            return _Banks.Find();
        }

        public IEnumerable<Banks> GelByAccountId(int accountId)
        {
            return _Banks.Find(BanksQuery.WithByAccountId(accountId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Banks"></param>
        public int CreateBankss(Banks Banks)
        {
            _Banks.Create(Banks);
            Context.SaveChanges();
            return Banks.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Banks"></param>
        public int UpdateBankss(Banks Banks)
        {
            _Banks.Update(Banks);
            return Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Banks"></param>
        /// <returns></returns>
        public int DeleteBankss(Banks Banks)
        {
            _Banks.Delete(Banks);
            return Context.SaveChanges();
        }
    }
}
