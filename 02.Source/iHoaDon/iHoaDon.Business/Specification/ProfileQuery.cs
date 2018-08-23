using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iHoaDon.Entities;

namespace iHoaDon.Business
{
    public class ProfileQuery
    {
        public static Expression<Func<Profile, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        public static Expression<Func<Profile, bool>> WithById(int id)
        {
            return al => al.Id == id;
        }
        public static Expression<Func<Profile, bool>> WithByProfileId(string companyCode)
        {
            return al => al.CompanyCode.Equals(companyCode);
        }
        public static Expression<Func<Profile, bool>> WithByCompanyCode(string companyCode)
        {
            return al => al.CompanyCode.Equals(companyCode);
        }
    }
}
