using System;
using System.Collections.Generic;
using System.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// Split the string using the specified delimiters
    /// </summary>
    public class SplitStringAttribute:PostRetrievalProcessingAttribute
    {
        /// <summary>
        /// Gets or sets the delimiters.
        /// </summary>
        /// <value>The delimiters.</value>
        public char[] Delimiters { get; set; }

        #region Overrides of PostRetrievalProcessingAttribute

        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public override object Process(string input, Type targetType)
        {
            var parts = input.Split(Delimiters ?? new[] {',', '|', ';'})
                            .Select(part => part.Trim())
                            .Where(part=>!String.IsNullOrEmpty(part))
                            .ToArray();
            if (targetType == typeof(List<string>))
            {
                return parts.ToList();
            }
            return parts;
        }

        #endregion
    }
}