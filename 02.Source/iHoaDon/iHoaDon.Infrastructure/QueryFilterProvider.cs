using System;
using System.Linq;
using System.Linq.Expressions;

namespace iHoaDon.Infrastructure
{
    /// <summary>
    /// A filter factory (pre/post filters)
    /// </summary>
    public abstract class QueryFilterProvider
    {
        /// <summary>
        /// Make a pagination filter (for paging the query, should be applied after a sort).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageAt">The page at.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="maxPageSize">Size of the max page.</param>
        /// <returns></returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> Page<T>(int pageAt = 1, int pageSize = 25, int maxPageSize = 50);

        /// <summary>
        /// Make a inclusion filter (for including related entities in the query).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paths">The paths.</param>
        /// <returns></returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> Include<T>(params string[] paths);

        /// <summary>
        /// Make a sort filter (for sorting the query result).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="sorter">The sorter.</param>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        /// <returns></returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> Sort<T,TKey>(Expression<Func<T, TKey>> sorter, bool descending = false);
    }
}