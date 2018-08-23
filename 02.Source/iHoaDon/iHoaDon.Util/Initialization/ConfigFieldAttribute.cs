using System;

namespace iHoaDon.Util
{
    /// <summary>
    /// This attribute contains the information needed to retrieve settings from the configuration source
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ConfigFieldAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFieldAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        public ConfigFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        /// <summary>
        /// Gets or sets the key of the setting, Initializer will look for this in the configuration source.
        /// </summary>
        /// <value>The key.</value>
        public string FieldName { get; private set; }

        /// <summary>
        /// Gets or sets the description for the setting.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this setting is required.
        /// If the setting is required but the query from the configuration source returns null and there is no default setting, the Initializer will throw up an exception
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the default value, this will be used in case the query from the configuration source returns null.
        /// </summary>
        /// <value>The default value.</value>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets the string value from the resolver.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns></returns>
        internal string GetStringValue(Func<string, string> resolver)
        {
            var result = resolver(FieldName) ?? DefaultValue;
            if (String.IsNullOrEmpty(result) && IsRequired)
            {
                throw new Exception("Attribute is required: " + FieldName); //we dont have it, we're screwed
            }
            return result;
        }
    }
}
