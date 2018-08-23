using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fasterflect;

namespace iHoaDon.Util
{
    /// <summary>
    /// For pumping an object (decorated with SchemaAttributes) with data
    /// </summary>
    public static class HydrationUtils
    {
        /// <summary>
        /// Hydrates the object with data from a dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The container.</param>
        /// <param name="data">The data.</param>
        public static void Hydrate<T>(object container, IDictionary<string, string> data)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            foreach (var property in typeof(T).Properties(Flags.Public|Flags.Instance))
            {
                var attr = property.Attribute<SchemaAttribute>();
                if (attr == null)
                {
                    continue;
                }

                if (attr is FieldAttribute)
                {
                    HydrateProperty(container, property, data, attr.Key);
                }
                else if (attr is TableAttribute)
                {
                    HydrateArray(container, data, property, attr);
                }
            }
        }

        private static void HydrateArray(object container, IDictionary<string, string> data, PropertyInfo property, SchemaAttribute attr)
        {
            if (!property.PropertyType.IsArray)
            {
                return;
            }

            var elemType = property.PropertyType.GetElementType();
            if (elemType == null)
            {
                return;
            }

            var colAttrs = elemType.Properties(Flags.Public | Flags.Instance)
                .Select(p => new
                {
                    Prop = p,
                    Field =    p.Attributes<FieldAttribute>()
                                .LastOrDefault()
                })
                .Where(pair => pair.Field != null)
                .ToDictionary(pair => pair.Field.Key, pair => pair.Prop);
            var colNames = colAttrs.Keys.ToArray();

            var max = colNames.Max(c => data.Keys.Count(k => k.StartsWith(c)));

            var array = Array.CreateInstance(elemType, max);

            var cellIdFormater = ((TableAttribute) attr).GetCellIndexFormatter();
            var start = ((TableAttribute) attr).StartIndex;
            for (var i = 0; i < max; i++, start++)
            {
                var elem = Activator.CreateInstance(elemType);
                foreach (var col in colNames)
                {
                    var fieldKey = cellIdFormater(start, col);
                    HydrateProperty(elem, colAttrs[col], data, fieldKey);
                }
                array.SetValue(elem, i);
            }
            property.SetValue(container, array, null);
        }

        private static void HydrateProperty(object container, PropertyInfo property, IDictionary<string, string> data, string key)
        {
            string rawData;
            if (!data.TryGetValue(key, out rawData))
            {
                return;
            }
            var converter = ConversionUtils.GetConverter(property.PropertyType, null);
            var value = converter(rawData);
            property.SetValue(container, value, null);
        }

        /// <summary>
        /// Extracts data from an object using reflection and the annotations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static IDictionary<string, object> ExtractData<T>(T container)
        {
            return ExtractData(container, typeof (T));
        }

        /// <summary>
        /// Extracts the data.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IDictionary<string, object> ExtractData(object container, Type type)
        {
            var result = new Dictionary<string, object>();
            foreach (var property in type.Properties(Flags.Public | Flags.Instance))
            {
                var attr = property.Attribute<SchemaAttribute>();
                if (attr == null)
                {
                    continue;
                }

                if (attr is FieldAttribute)
                {
                    result.Add(attr.Key, property.GetValue(container, null));
                }
                else if (attr is TableAttribute)
                {
                    var collection = property.GetValue(container, null) as IEnumerable;
                    if (collection == null)
                    {
                        continue;
                    }
                    var rows = collection
                                .Cast<object>()
                                .Where(o => o != null)
                                .SelectMany(o => o.GetType()
                                                    .Properties(Flags.Instance | Flags.Public)
                                                    .Where(prop => prop.HasAttribute<FieldAttribute>())
                                                    .Select(prop => new Dictionary<string, object>
                                                                        {
                                                                            {
                                                                                prop.Attribute<FieldAttribute>().Key,
                                                                                prop.GetValue(o, null)
                                                                                }
                                                                        }));
                    result.Add(attr.Key, rows);
                }
            }
            return result;
        }
    }
}