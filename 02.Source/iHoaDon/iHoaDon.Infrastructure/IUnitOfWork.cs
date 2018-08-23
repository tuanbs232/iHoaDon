using System;
using System.Collections.Generic;

namespace iHoaDon.Infrastructure
{
    /// <summary>
    /// Coi như một giao dịch dữ liệu
    /// Quản lý thêm, sửa, xóa trên nhiều bảng khác nhau
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// Saves the changes to all entities that was made in the context of this IUnitOfWork.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Gets the repository for the given type of entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T:class;

        /// <summary>
        /// Send raw sql command to the datasource
        /// </summary>
        /// <param name="queryText">The query text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IEnumerable<dynamic> RawQuery(string queryText, params object[] parameters);

        /// <summary>
        /// Send raw modification command to the datasource
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int RawModify(string commandText, params object[] parameters);

        /// <summary>
        /// Raws the scalar.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        object RawScalar(string commandText, params object[] parameters);

        /// <summary>
        /// Gets the filters specific to the underlying data source.
        /// </summary>
        /// <value>The filters.</value>
        QueryFilterProvider Filters { get; }


    }
}