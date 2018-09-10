using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    public class TransactionService : Service
    {
        private readonly IRepository<Transaction> _transaction;
        public TransactionService(IUnitOfWork context)
            : base(context)
        {
            _transaction = Context.GetRepository<Transaction>();
        }

        public Transaction GetById(int id)
        {
            return _transaction.One(TransactionQuery.WithById(id));
        }

        public Transaction GetByInvoiceId(int id)
        {
            return _transaction.One(TransactionQuery.WithByInvoiceId(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public int CreateTransactions(Transaction transaction)
        {
            _transaction.Create(transaction);
            Context.SaveChanges();
            return transaction.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public int UpdateTransactions(Transaction transaction)
        {
            _transaction.Update(transaction);
            return Context.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int DeleteTransactions(Transaction transaction)
        {
            _transaction.Delete(transaction);
            return Context.SaveChanges();
        }
    }
}
