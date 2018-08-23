using System;

namespace iHoaDon.Business
{
    /// <summary>
    /// Delegate Utilities
    /// </summary>
    public static class DelegateUtil
    {
        /// <summary>
        /// Repeat the action n times.
        /// </summary>
        /// <param name="n">The times.</param>
        /// <param name="action">The action.</param>
        public static void Times(this int n, Action action)
        {
            if(action == null || n <= 0)
            {
                return;
            }

            for (var i = 0; i < n; i++)
            {
                action();
            }
        }
    }
}