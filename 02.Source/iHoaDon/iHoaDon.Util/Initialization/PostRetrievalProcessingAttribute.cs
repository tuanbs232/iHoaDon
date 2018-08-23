using System;

namespace iHoaDon.Util
{
    /// <summary>
    /// This is the step that will be execute after all pre-processing.
    /// The value returned from this one's Process method shall be assigned to the decorated property.
    /// If one decorator of this kind is presence, the Initializer will forgo the TypeConverter in favor of this one
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public abstract class PostRetrievalProcessingAttribute:Attribute
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public abstract object Process(string input, Type targetType);
    }
}