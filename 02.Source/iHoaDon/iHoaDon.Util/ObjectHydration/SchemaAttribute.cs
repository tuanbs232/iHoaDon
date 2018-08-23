using System;

namespace iHoaDon.Util
{
    /// <summary>
    /// A schema (can be a field or a table)
    /// </summary>
    public abstract class SchemaAttribute:Attribute
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; protected set; }
    }
}