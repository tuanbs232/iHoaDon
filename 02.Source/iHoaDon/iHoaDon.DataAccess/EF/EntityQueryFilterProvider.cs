using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using iHoaDon.Infrastructure;

namespace iHoaDon.DataAccess
{
    /// <summary>
    /// Provide filters specific to EntityFramework
    /// </summary>
    public class EntityQueryFilterProvider : QueryFilterProvider
    {
        #region Overrides of QueryFilterProvider
        /// <summary>
        /// Paginate the input IQueryable.
        /// NOTE: it is always best practice to a sort before any pagination
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageAt">The page at.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="maxPageSize">Maximum size of the page.</param>
        /// <returns></returns>
        public override Func<IQueryable<T>, IQueryable<T>> Page<T>(int pageAt = 1, int pageSize = 25, int maxPageSize = 50)
        {
            var myPage = pageAt < 1 ? 1 : pageAt;
            var myPageSize = pageSize <= 0 || pageSize > maxPageSize ? maxPageSize : pageSize;
            return source => source.Skip((myPage - 1) * pageSize).Take(myPageSize);
        }

        /// <summary>
        /// Specify that the IQueryable should include some related entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paths">The path(s) to the related entities.</param>
        /// <returns></returns>
        public override Func<IQueryable<T>, IQueryable<T>> Include<T>(params string[] paths)
        {
            if (paths == null || paths.Length <= 0)
            {
                return q => q;
            }
            return q =>
            {
                var dbQuery = q as DbQuery<T>;
                if (dbQuery == null)
                {
                    return q;
                }
                dbQuery = paths.Where(i => !String.IsNullOrEmpty(i))
                    .Aggregate(dbQuery, (current, incl) => current.Include(incl));
                return dbQuery;
            };
        }

        /// <summary>
        /// Sort the IQueryable using the specified sorter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="sorter">The sort.</param>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        /// <returns></returns>
        public override Func<IQueryable<T>, IQueryable<T>> Sort<T, TKey>(Expression<Func<T, TKey>> sorter, bool descending = false)
        {
            return source => descending ? source.OrderByDescending(sorter) : source.OrderBy(sorter);
        }

        /// <summary>
        /// Creates the sort.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        /// <param name="sortExprs">The sort exprs.</param>
        /// <returns></returns>
        public Func<IQueryable<T>, IQueryable<T>> CreateSort<T>(bool descending = false, params string[] sortExprs)
        {
            return source =>
            {
                var type = typeof (T);
                var isFirst = true;
                foreach (var sortExpr in sortExprs)
                {
                    var param = Expression.Parameter(type, type.Name.ToLower());
                    var sort = Expression.Lambda<Func<T, object>>(Expression.Property(param, sortExpr), param);

                    if (isFirst)
                    {
                        source = source.OrderBy(sort);
                        isFirst = false;
                    }
                    else
                    {
                        source = ((IOrderedQueryable<T>) source).ThenBy(sort);
                    }
                }
                return source;
            };
        }

        #endregion
    }
}