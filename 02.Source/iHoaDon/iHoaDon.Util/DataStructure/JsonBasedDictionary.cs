using System.Collections.Generic;

namespace iHoaDon.Util
{
    /// <summary>
    /// A json-based dictionary (a store with a front like a dictionary, but can save data to a json string)
    /// </summary>
    public class JsonBasedDictionary:StringBasedDictionary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonBasedDictionary"/> class.
        /// </summary>
        /// <param name="initial">The initial.</param>
        public JsonBasedDictionary(string initial) : base(initial){}

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="embedTypeInfo">if set to <c>true</c> [embed type info].</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString(bool embedTypeInfo)
        {
            return embedTypeInfo ? Data.Stringify() : Data.StringifyJs();
        }

        /// <summary>
        /// Create a dictionary from the string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        protected override IDictionary<string, object> FromString(string input)
        {
            return Json2.ParseAs<IDictionary<string, object>>(input);
        }
    }
}