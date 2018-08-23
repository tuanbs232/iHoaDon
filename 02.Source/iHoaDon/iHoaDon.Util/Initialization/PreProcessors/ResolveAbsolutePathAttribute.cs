using System;
using System.Configuration;

namespace iHoaDon.Util
{
    /// <summary>
    /// Resolve absolute path
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class ResolveAbsolutePathAttribute:PreRetrievalProcessingAttribute
    {
        /// <summary>
        /// Gets or sets the base path.
        /// </summary>
        /// <value>The base path.</value>
        public string BasePath { get; set; }

        /// <summary>
        /// Gets or sets the app config base path key.
        /// </summary>
        /// <value>The app config base path key.</value>
        public string AppConfigBasePathKey { get; set; }

        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public override string Process(string input)
        {
            string basePath = null;
            if(!String.IsNullOrEmpty(BasePath))
            {
                basePath = BasePath;
            } 
            else if(!String.IsNullOrEmpty(AppConfigBasePathKey))
            {
                basePath = ConfigurationManager.AppSettings.Get(AppConfigBasePathKey);
            }
            return PathUtil.ToAbsolute(input, basePath);
        }
    }
}