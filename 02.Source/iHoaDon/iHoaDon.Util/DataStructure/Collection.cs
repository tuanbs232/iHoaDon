using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// Utilities for collections
    /// </summary>
    public static class Collection
    {
        /// <summary>
        /// Map the collection to a dictionary safely.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK">The type of the K.</typeparam>
        /// <typeparam name="TV">The type of the V.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="getKey">The get key.</param>
        /// <param name="getVal">The get val.</param>
        /// <returns></returns>
        public static IDictionary<TK, TV> ToDictionarySafely<T, TK, TV>(this IEnumerable<T> source, Func<T, TK> getKey, Func<T, TV> getVal)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (getKey == null)
            {
                throw new ArgumentNullException("getKey");
            }
            if (getVal == null)
            {
                throw new ArgumentNullException("getVal");
            }
            var result = new Dictionary<TK, TV>();
            source.ForEach(item => result.Upsert(getKey(item), getVal(item)));
            return result;
        }

        /// <summary>
        /// Merges the specified dictionaries.
        /// </summary>
        /// <param name="dicts">The dicts.</param>
        /// <returns></returns>
        public static IDictionary<string, object> Merge(params IDictionary<string, object>[] dicts)
        {
            var result = new Dictionary<string, object>();

            foreach (var dict in dicts)
            {
                if (dict == null)
                {
                    continue;
                }
                foreach (var key in dict.Keys)
                {
                    if (key == null)
                    {
                        continue;
                    }
                    result.Upsert(key, dict[key]);
                }
            }
            return result;
        }

        /// <summary>
        /// .ForEach() extension for IEnumerable of T (previously, only List of T has this method).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (action == null) { throw new ArgumentNullException("action"); }
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// .ForEach() extension for IEnumerable of T (previously, only List of T has this method).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (action == null) { throw new ArgumentNullException("action"); }
            var index = 0;
            foreach (var item in source)
            {
                action(item, index);
                index++;
            }
        }

        /// <summary>
        /// Paginate the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageAt">The page at.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="maxPageSize">Size of the max page.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageAt, int pageSize, int maxPageSize)
        {
            var myPage = pageAt < 1 ? 1 : pageAt;
            var myPageSize = pageSize <= 0 || pageSize > maxPageSize ? maxPageSize : pageSize;
            return source.Skip((myPage - 1) * pageSize).Take(myPageSize);
        }

        /// <summary>
        /// Insert or update the provided key-value-pair to the dictionary
        /// </summary>
        /// <typeparam name="TK">The type of the K.</typeparam>
        /// <typeparam name="TV">The type of the V.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Upsert<TK,TV>(this IDictionary<TK,TV> dictionary, TK key, TV value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Convert a collection of T to a DataTable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            
            var fields = typeof (T).GetProperties()
                                    .Select(p => p.Name)
                                    .ToArray();
            
            var cols = fields.Select(f => new DataColumn(f))
                            .ToArray();
            var result = new DataTable();
            result.Columns.AddRange(cols);

            foreach (var item in source)
            {
                if (item == null)
                {
                    continue;
                }
                var row = result.NewRow();
                foreach (var col in cols)
                {
                    var data = typeof(T).GetProperty(col.ColumnName).GetValue(item, null);
                    row[col] = data;
                }
                result.Rows.Add(row);
            }
            return result;
        }
    }
}
