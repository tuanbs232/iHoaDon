using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// Conversion utils
    /// </summary>
    public static class ConversionUtils
    {
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
        private const string DateTimeFormat = @"dd/MM/yyyy";
        
        #region Conversion steps
        /// <summary>
        /// Gets the converter for the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static Func<object, object> GetConverter(Type type, string format)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            
            Func<object, object> result;
            if (type == typeof(object))
            {
                result = data => data;
            }
            else if (type == typeof(string))
            {
                result = data => data == null ? null : data.ToString();
            }
            else if (type == typeof(bool))
            {
                result = data => ConvertBoolean(data);
            }
            else if (type == typeof(long))
            {
                result = data => ConvertInt64(data);
            }
            else if (type == typeof(decimal))
            {
                result = data => ConvertDecimal(data);
            }
            else if (type == typeof(DateTime))
            {
                result = data => ConvertDateTime(data,format);
            }
            else if (type == typeof(byte[]))
            {
                result = data => ConvertBytes(data);
            }
            else if (type == typeof(IEnumerable))
            {
                result = data => ConvertIEnumerable(data);
            }
            else
            {
                result = data => ConvertWithTypeConnverter(type, data);
            }
            
            return result;
        }
        #endregion

        #region Static conversions
        /// <summary>
        /// Convert the input to byte[].
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static byte[] ConvertBytes(object data)
        {
            byte[] result;
            if (data == null)
            {
                result = null;
            }
            else if (data is byte[])
            {
                result = data as byte[];
            }
            else if (data is string)
            {
                result = Convert.FromBase64String(data.ToString());
            }
            else
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Convert input as DateTime using the specified format.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        internal static DateTime ConvertDateTime(object data, string format)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            return DateTime.ParseExact(
                data.ToString(),
                String.IsNullOrEmpty(format) ? DateTimeFormat : format,
                Culture.DateTimeFormat,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.RoundtripKind
            );
        }

        /// <summary>
        /// Converts input to decimal.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static decimal ConvertDecimal(object data)
        {
            return data == null
                    ? 0
                    : Decimal.Parse(
                        data.ToString()
                            .Replace(" ", String.Empty),
                        NumberStyles.Currency
                    );
        }

        /// <summary>
        /// Convert input to boolean.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static bool ConvertBoolean(object data)
        {
            bool result;
            if (data == null)
            {
                result = false;
            }
            else if (data is bool)
            {
                result = (bool)data;
            }
            else if (data is string)
            {
                var inputStr = data.ToString().Trim();
                var falseStrs = new[] { "false", "off", "0", "no", "n", "không", "sai", "không có", String.Empty };
                //Note:this semantics is risky if the data source is not html form submit
                result = !falseStrs.Any(falseStr => String.Equals(falseStr, inputStr, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Converts input to Int64.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static long ConvertInt64(object data)
        {
            return data == null
                    ? 0
                    : Int64.Parse(
                        data.ToString()
                            .Replace(" ", String.Empty),
                        NumberStyles.Integer | NumberStyles.AllowThousands
                    );
        }

        /// <summary>
        /// Converts the input to IEnumerable.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static IEnumerable ConvertIEnumerable(object data)
        {
            IEnumerable result;
            if (data == null)
            {
                result = null;
            }
            else if (data is string)
            {
                result = StringExtension.Split(data.ToString(), new[] { ',' });
            }
            else if (data is IEnumerable)
            {
                result = data as IEnumerable;
            }
            else
            {
                result = Enumerable.Empty<object>();
            }
            return result;
        }

        internal static object ConvertWithTypeConnverter(Type type, object data)
        {
            var converter = TypeDescriptor.GetConverter(type);
            if (converter == null)
            {
                throw new InvalidOperationException("Converter should never be null");
            }
            return converter.ConvertFrom(null, Culture, data);
        }

        /// <summary>
        /// Gets the type (that inherits from T) from the given name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Type GetType<T>(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Custom Type name should be specified when the data type is set to Custom");
            }

            var type = Type.GetType(name, false, true);
            if (type == null)
            {
                throw new ArgumentException("Invalid type name: " + name);
            }

            if (!type.IsSubclassOf(typeof(T)))
            {
                throw new ArgumentException("Must inherit from " + typeof(T).Name);
            }
            return type;
        }
        #endregion
    }
}