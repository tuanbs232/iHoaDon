using iHoaDon.Infrastructure;

namespace iHoaDon.Business
{
    /// <summary>
    /// I handle a web form
    /// </summary>
    public interface IFFormHandler
    {
        /// <summary>
        /// Handles the specified form.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        void Handle(string key, object data, IUnitOfWork context);
    }
}