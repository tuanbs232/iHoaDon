using System;
using System.Linq.Expressions;

namespace iHoaDon.Entities
{
    /// <summary>
    /// Specifications for AccountLogQuery
    /// </summary>
    public static class LogQuery
    {
        /// <summary>
        /// Gell all
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<AccountLogin, bool>> WithAll()
        {
            return al=>al.Id!=0;
        }
        /// <summary>
        /// LoginName contains loginName
        /// </summary>
        /// <param name="loginName">The login name.</param>
        /// <returns></returns>
        public static Expression<Func<AccountLogin, bool>> WithLoginName(string loginName)
        {
            return al => al.LoginName.Contains(loginName);
        }

        /// <summary>
        /// Withes from date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns></returns>
        public static Expression<Func<AccountLogin, bool>> WithFromDate(DateTime? fromDate)
        {
            return al => al.LoginTime >= fromDate;
        }


        /// <summary>
        /// Withes to date.
        /// </summary>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public static Expression<Func<AccountLogin, bool>> WithToDate(DateTime? toDate)
        {
            return al => al.LoginTime <= toDate;
        }

        /// <summary>
        /// Withes to status.
        /// </summary>
        /// <param name="status">To status.</param>
        /// <returns></returns>
        public static Expression<Func<AccountLogin, bool>> WithStatus(bool? status)
        {
            return al => al.Status == status;
        }

        /// <summary>
        /// Gell all
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithAllAct()
        {
            return al => al.Id != 0;
        }
        /// <summary>
        /// Gell all
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithActionTypeAct(LogActionType actionType)
        {
            return al => al.ActionType ==(byte)actionType;
        }
        /// <summary>
        /// ID = Id
        /// </summary>
        /// <param name="id">The login name.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithIdAct(int id)
        {
            return al => al.Id == id;
        }

        /// <summary>
        /// LoginName LIKE %loginName%
        /// </summary>
        /// <param name="loginName">The login name.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithLoginNameAct(string loginName)
        {
            return al => al.LoginName.Contains(loginName);
        }

        /// <summary>
        /// ActionContent LIKE %actionContent%
        /// </summary>
        /// <param name="actionContent">The action content.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithActionContentAct(string actionContent)
        {
            return al => al.ActionContent.Contains(actionContent);
        }

        /// <summary>
        /// Withes from date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithFromDateAct(DateTime? fromDate)
        {
            return al => al.ActionTime >= fromDate;
        }


        /// <summary>
        /// Withes to date.
        /// </summary>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithToDateAct(DateTime? toDate)
        {
            return al => al.ActionTime <= toDate;
        }
    }
}
