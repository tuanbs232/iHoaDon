using System;
using System.IO;
using iHoaDon.Util;

namespace iHoaDon.Util
{
    /// <summary>
    /// Deserialize input as Json
    /// </summary>
    public class DeserializeJsonAttribute:PostRetrievalProcessingAttribute
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public override object Process(string input, Type targetType)
        {
            using (var reader = File.OpenText(input))
            {
                return Json2.Parse(reader, targetType);
            }
        }
    }
}