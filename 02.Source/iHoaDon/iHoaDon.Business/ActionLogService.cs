using iHoaDon.Entities;
using iHoaDon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iHoaDon.Business
{
    /// <summary>
    /// Action log
    /// </summary>
    public class ActionLogService : Service
    {
        private readonly IRepository<ActionLog> _actionLog;
        private readonly IRepository<AccountLogin> _items;
        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="ctx"></param>
        public ActionLogService(IUnitOfWork ctx)
            : base(ctx)
        {
            _actionLog = Context.GetRepository<ActionLog>();
            _items = Context.GetRepository<AccountLogin>();
        }
        /// <summary>
        /// Create the action log
        /// </summary>
        /// <param name="loginName">The login name</param>
        /// <param name="actionContent">The action content</param>
        /// <param name="dataBeforeChange">The data before change</param>
        /// <param name="dataAfterChange"></param>
        /// <param name="actionType"></param>
        public void CreateActionLog(string loginName, string actionContent, byte actionType, string dataBeforeChange = "", string dataAfterChange = "")
        {
            var actionLog = new ActionLog
            {
                LoginName = loginName,
                ActionContent = actionContent,
                DataBeforeChange = dataBeforeChange,
                DataAfterChange = dataAfterChange,
                ActionTime = DateTime.Now,
                ActionType = actionType
            };
            _actionLog.Create(actionLog);
            Context.SaveChanges();
        }
        /// <summary>
        /// Get Action Log by Id
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns></returns>
        public ActionLog GetById(int id)
        {
            var result = _actionLog.One(LogQuery.WithIdAct(id));
            if (result == null)
            {
                throw new Exception("No records");
            }
            return result;
        }
        /// <summary>
        /// Gets all action log.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        /// <param name="loginName">The login name.</param>
        /// <param name="actionContent">Action Content.</param>
        /// <param name="actionType">Action Type.</param>
        /// <returns></returns>
        public IEnumerable<ActionLog> GetAllActionLogItems(out int totalRecords,
                                                                    int currentPage = 1,
                                                                    int pageSize = 25,
                                                                    bool descending = true,
                                                                    string sortBy = "ActionTime",
                                                                    string loginName = "",
                                                                    string actionContent = "",
                                                                    LogActionType? actionType = null)
        {

            var spec = LogQuery.WithAllAct();
            spec = !string.IsNullOrEmpty(loginName) ? spec.And(LogQuery.WithLoginNameAct(loginName)) : spec;
            spec = !string.IsNullOrEmpty(actionContent) ? spec.And(LogQuery.WithActionContentAct(actionContent)) : spec;
            spec = actionType.HasValue ? spec.And(LogQuery.WithActionTypeAct(actionType.Value)) : spec;
            totalRecords = _actionLog.Count(spec);
            var sort = Context.Filters.Sort<ActionLog, DateTime>(al => al.ActionTime, true);
            switch (sortBy)
            {
                case "LoginName":
                    sort = Context.Filters.Sort<ActionLog, string>(al => al.LoginName, descending);
                    break;
                case "ActionContent":
                    sort = Context.Filters.Sort<ActionLog, string>(al => al.ActionContent, descending);
                    break;
                case "ActionTime":
                    sort = Context.Filters.Sort<ActionLog, DateTime>(al => al.ActionTime, descending);
                    break;
            }
            var pager = Context.Filters.Page<ActionLog>(currentPage, pageSize);

            return _actionLog.Find(spec, sort, pager);
        }
        /// <summary>
        /// Gets all account log  LINQ.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        /// <param name="loginName">The login name.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public IEnumerable<AccountLogin> GetAllLogItems(out int totalRecords,
                                                                    int currentPage = 1,
                                                                    int pageSize = 25,
                                                                    bool descending = true,
                                                                    string sortBy = "LoginTime",
                                                                    string loginName = "",
                                                                    DateTime? fromDate = null,
                                                                    DateTime? toDate = null,
                                                                    bool? status = null
                                                                    )
        {

            var spec = LogQuery.WithAll();
            spec = !string.IsNullOrEmpty(loginName) ? LogQuery.WithLoginName(loginName) : spec;
            spec = fromDate != null
                    ? spec.And(LogQuery.WithFromDate(fromDate))
                    : spec;
            spec = toDate.HasValue
                    ? spec.And(LogQuery.WithToDate(toDate.Value.AddDays(1)))
                    : spec;
            spec = status != null
                    ? spec.And(LogQuery.WithStatus(status))
                    : spec;

            totalRecords = _items.Count(spec);
            var sort = Context.Filters.Sort<AccountLogin, DateTime>(al => al.LoginTime, true);
            switch (sortBy)
            {
                case "LoginTime":
                    sort = Context.Filters.Sort<AccountLogin, DateTime>(al => al.LoginTime, descending);
                    break;
                case "LoginIP":
                    sort = Context.Filters.Sort<AccountLogin, string>(al => al.LoginIP, descending);
                    break;
                case "LoginName":
                    sort = Context.Filters.Sort<AccountLogin, string>(al => al.LoginName, descending);
                    break;
            }
            var pager = Context.Filters.Page<AccountLogin>(currentPage, pageSize);

            return _items.Find(spec, sort, pager);
        }
        /// <summary>
        /// Get All Log By TaxNumber
        /// </summary>
        /// <param name="taxNumber"></param>
        /// <returns></returns>
        public IEnumerable<ActionLog> GetAllLogByTaxNumber(string taxNumber)
        {
            var spec = LogQuery.WithActionContentAct(taxNumber);
            if (spec == null)
            {
                throw new Exception("Không có nhật ký cho tài khoản: " + taxNumber);
            }
            return _actionLog.Find(spec);
        }
    }
}
