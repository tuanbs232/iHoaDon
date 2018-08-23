using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using iHoaDon.Infrastructure;

namespace iHoaDon.DataAccess
{
    /// <summary>
    /// A repository of entities (a table)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EWhiteHatRepository<T> : IRepository<T> where T : class
    {
        private readonly EWhiteHatContext _ctx;
        private readonly DbSet<T> _set;

        /// <summary>
        /// Initializes a new instance of the <see cref="EWhiteHatRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context (IUnitOfWork).</param>
        public EWhiteHatRepository(IUnitOfWork context)
        {
            var evan = context as EWhiteHatContext;
            if (evan == null)
            {
                throw new ArgumentException("context must be of type Evan");
            }
            _ctx = evan;
            _set = _ctx.Set<T>();
            if (_set == null)
            {
                throw new Exception("Entity type is not defined:" + typeof(T).Name);
            }
        }

        /// <summary>
        /// Gets the raw IQueryable of T (upon which you can issue various Linq-to-Entities queries not defined in this API).
        /// </summary>
        /// <value>The raw.</value>
        public IQueryable<T> Raw
        {
            get { return _set; }
        }

        /// <summary>
        /// Finds all the entities matching the specified specification.
        /// NOTE: no spec given means you want all elements
        /// </summary>
        /// <param name="spec">The specification.</param>
        /// <param name="preFilter">The pre filter (which alter/filter the datasource before the query is put against it. Example: inclusion of related path).</param>
        /// <param name="postFilters">The post filters (which filter the datasource after the query has been performed).</param>
        /// <returns></returns>
        public IEnumerable<T> Find( Expression<Func<T, bool>> spec                  = null, 
                                    Func<IQueryable<T>, IQueryable<T>> preFilter    = null, 
                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            return FindCore(spec, preFilter, postFilters).ToList();
        }

        /// <summary>
        /// Finds all the entities matching the specified specification, then convert the result into another type
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="projector">The projector.</param>
        /// <param name="spec">The specification.</param>
        /// <param name="preFilter">The pre filter (which alter/filter the datasource before the query is put against it. Example: inclusion of related path).</param>
        /// <param name="postFilters">The post filters (which filter the datasource after the query has been performed).</param>
        /// <returns></returns>
        public IEnumerable<TOutput> FindAs<TOutput>(Expression<Func<T, TOutput>> projector, 
                                                    Expression<Func<T, bool>> spec                  = null, 
                                                    Func<IQueryable<T>, IQueryable<T>> preFilter    = null, 
                                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            if (projector == null)
            {
                throw new ArgumentNullException("projector");
            }
            return FindCore(spec, preFilter, postFilters).Select(projector).ToList();
        }

        /// <summary>
        /// Gets one entity with the specified id(s).
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public T One(params object[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentException("no id specified");
            }
            return _set.Find(ids);
        }

        /// <summary>
        /// Gets one entity matching the given specification.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public T One(Expression<Func<T, bool>> spec)
        {
            if (spec == null)
            {
                throw new ArgumentNullException("spec");
            }
            return _set.SingleOrDefault(spec); 
        }

        /// <summary>
        /// Gets one entity matching the given specification, then convert it to another type via the provided converter function.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="projector">The projector.</param>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public TOutput OneAs<TOutput>(  Expression<Func<T, TOutput>> projector, 
                                        Expression<Func<T, bool>> spec)
        {
            if (projector == null)
            {
                throw new ArgumentNullException("projector");
            }
            if (spec == null)
            {
                throw new ArgumentNullException("spec");
            }
            return _set.Where(spec)
                        .Select(projector)
                        .SingleOrDefault();
        }

        /// <summary>
        /// Is there any entity in the repository that satisfy the given spec?
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? _set.Any() : _set.Any(spec);
        }

        /// <summary>
        /// How many elements are there that matches the specification?
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? _set.Count() : _set.Count(spec);
        }

        /// <summary>
        /// Creates a new entity (which will be tracked by the framework and saved later, when you call SaveChange on the IUnitOfWork).
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Create(T entity)
        {
            _set.Add(entity);
        }

        /// <summary>
        /// Note: calling Update on a Repository within the context of a IUnitOfWork is not advisable.
        /// When trying to update a Repository within an IUnitOfWork, you should:
        /// 1. Select the object 
        /// 2. Modify it within the context of the IUnitOfWork
        /// 3. SaveChanges (the modification you made is tracked)
        /// Note: Only call this when you are sure you want to issue an UPDATE without issuing a SELECT first
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            _set.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// NOTE: the entity should be tracked by the framework before deletion: always do a select before a delete
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        private IQueryable<T> FindCore(Expression<Func<T, bool>> spec, Func<IQueryable<T>,IQueryable<T>> preFilter, params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            IQueryable<T> result = preFilter != null ? preFilter(_ctx.Set<T>()) : _ctx.Set<T>();
            if (spec != null)
            {
                result = result.Where(spec);
            }
            foreach (var postFilter in postFilters)
            {
                if (postFilter != null)
                {
                    result = postFilter(result);
                }
            }
            return result;
        }
    }
}