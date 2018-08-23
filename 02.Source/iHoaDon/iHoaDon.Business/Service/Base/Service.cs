using System;
using System.Security.Principal;
using System.Threading;
using iHoaDon.Entities;
using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    /// <summary>
    /// A service
    /// </summary>
    public abstract class Service: IDisposable
    {
        /// <summary>
        /// The context (IUnitOfWork)
        /// NOTE: call Context.SaveChanges() after you've done some changes to the data and need to persist them to the database
        /// </summary>
        protected readonly IUnitOfWork Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        protected Service(IUnitOfWork ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }
            Context = ctx;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>The user.</value>
        public IPrincipal User
        {
            get { return Thread.CurrentPrincipal; }
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}