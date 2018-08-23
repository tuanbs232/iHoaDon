using iHoaDon.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace iHoaDon.Business
{
    public class TaxRateQuerry
    {
        public static Expression<Func<TaxRate, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<TaxRate, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
    }
}
