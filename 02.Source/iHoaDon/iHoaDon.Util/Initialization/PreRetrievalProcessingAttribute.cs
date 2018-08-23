using System;

namespace iHoaDon.Util
{
    /// <summary>
    /// Perform a processing step before the value from the configuration source is finally casted into the decorated property's type
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public abstract class PreRetrievalProcessingAttribute:Attribute
    {
        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether any exception thrown in the process should be suppressed.
        /// </summary>
        /// <value><c>true</c> if [suppress exception]; otherwise, <c>false</c>.</value>
        public bool SuppressException { get; set; }

        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public abstract string Process(string input);
    }
}