using System;

namespace iHoaDon.Util
{
    /// <summary>
    /// A table
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class TableAttribute : SchemaAttribute
    {
        /// <summary>
        /// Gets or sets the cell index format.
        /// </summary>
        /// <value>The cell index format.</value>
        public string CellIndexFormat { get; set; }

        /// <summary>
        /// Gets or sets the start index.
        /// </summary>
        /// <value>The start index.</value>
        public int StartIndex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="itemType">Type of the item.</param>
        public TableAttribute(string key, Type itemType)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if(itemType == null)
            {
                throw new ArgumentNullException("itemType");
            }
            Key = key;
            ItemType = itemType;
        }

        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        public Type ItemType { get; private set; }

        /// <summary>
        /// Gets the cell index formatter.
        /// </summary>
        /// <returns></returns>
        public Func<int, string, string> GetCellIndexFormatter()
        {
            return (index, k) => k + (String.IsNullOrEmpty(CellIndexFormat)
                                        ? index.ToString()
                                        : CellIndexFormat.FormatWith(index));
        }
    }
}