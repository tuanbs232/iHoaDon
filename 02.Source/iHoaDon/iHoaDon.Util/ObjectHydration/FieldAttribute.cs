using System;

namespace iHoaDon.Util
{
    /// <summary>
    /// A field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class FieldAttribute : SchemaAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public FieldAttribute(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            Key = key;
        }
    }
}