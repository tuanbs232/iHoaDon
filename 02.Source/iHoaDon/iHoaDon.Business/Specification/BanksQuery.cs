using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class BanksQuery
    {

        public static Expression<Func<Banks, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Banks, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
       

        public static Expression<Func<Banks, bool>> WithByAccountId(int accountId)
        {
            return al => al.AccountId == accountId;
        }

        

            
    }
}
