using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace iHoaDon.Entities
{
    /// <summary>
    /// Query action log
    /// </summary>
    public class ActionLogQuery
    {
        /// <summary>
        /// Gell all
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithAll()
        {
            return al => al.Id != 0;
        }
        /// <summary>
        /// ID = Id
        /// </summary>
        /// <param name="Id">The login name.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithId(int Id)
        {
            return al => al.Id==Id;
        }

        /// <summary>
        /// LoginName LIKE %loginName%
        /// </summary>
        /// <param name="loginName">The login name.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithLoginName(string loginName)
        {
            return al => al.LoginName.Contains(loginName);
        }

        /// <summary>
        /// ActionContent LIKE %actionContent%
        /// </summary>
        /// <param name="actionContent">The action content.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLog, bool>> WithActionContent(string actionContent)
        {
            return al => al.ActionContent.Contains(actionContent);
        }
    }
}
