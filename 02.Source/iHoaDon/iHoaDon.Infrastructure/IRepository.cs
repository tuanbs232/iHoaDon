using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace iHoaDon.Infrastructure
{
    /// <summary>
    /// A repository of type T
    /// E.g. A repository of Product, Customer...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// Finds all items  in the repository matching a given specification.
        /// </summary>
        /// <param name="spec">A specification (or spec) is just a predicate (delegate that take an item and returns true/false). A specification can be (in words): has id = 3, has price > 20000, has content that is not null or empty... When a spec is null/omitted, the Repo implementation should returns all items.</param>
        /// <param name="preFilter">A prefilter: apply some custom modification to the underlying IQueryable of T before the actual specification(s) is applied. (e.g. L2S's loadWith and EF's Include)</param>
        /// <param name="postFilter">A post filter: same as preFilter, just applied after the specifications. </param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T,bool>> spec = null,
                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Find all items in the repository matching a given specification. The result is mapped to another type using a user-supplied mapper.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="projector">A projector is a converter that map from one type to another (e.g. a SELECT statement on a SQL system).</param>
        /// <param name="spec">A specification (or spec) is just a predicate (delegate that take an item and returns true/false). A specification can be (in words): has id = 3, has price > 20000, has content that is not null or empty... When a spec is null/omitted, the Repo implementation should returns all items.</param>
        /// <param name="preFilter">A prefilter: apply some custom modification to the underlying IQueryable of T before the actual specification(s) is applied. (e.g. L2S's loadWith and EF's Include)</param>
        /// <param name="postFilter">A post filter: same as preFilter, just applied after the specifications. </param>
        /// <returns></returns>
        IEnumerable<TOutput> FindAs<TOutput>(   Expression<Func<T, TOutput>> projector, 
                                                Expression<Func<T, bool>> spec = null, 
                                                Func<IQueryable<T>, IQueryable<T>> preFilter = null, 
                                                params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Find one item with the supplied identification info.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        T One(params object[] ids);

        /// <summary>
        /// Find one item matching the give spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        T One(Expression<Func<T, bool>> spec);

        /// <summary>
        /// Find one item matching the given spec, converting it to another type.
        /// </summary>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="projector">The projector.</param>
        /// <param name="specs">The specs.</param>
        /// <returns></returns>
        TOutput OneAs<TOutput>(Expression<Func<T, TOutput>> projector, 
                                Expression<Func<T, bool>> specs = null);

        /// <summary>
        /// Determine whether the repository has any item matching a given spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Count the number of items in the repository matching a given spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Creates a new item in the repository (will be saved when calling SaveChanges on the IUnitOfWork).
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Create(T entity);

        /// <summary>
        /// Attach an entity from outside, (will be saved when calling SaveChanges on the IUnitOfWork).
        /// NOTE: this should not be called.
        /// The standard procedure is to Retrieve the item first, then modify it under the supervision of the IUnitOfWork, then save it.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity. (will be saved when calling SaveChanges on the IUnitOfWork).
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets the raw IQueryable.
        /// </summary>
        /// <value>The raw.</value>
        IQueryable<T> Raw { get; }
    }
}