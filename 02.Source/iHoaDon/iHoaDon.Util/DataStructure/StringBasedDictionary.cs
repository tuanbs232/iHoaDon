using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// A dictionary that serializes itself to a string
    /// </summary>
    public abstract class StringBasedDictionary:IDictionary<string,object>
    {
        private readonly string _initialJson;
        private IDictionary<string, object> _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBasedDictionary"/> class.
        /// </summary>
        /// <param name="json">The json.</param>
        protected StringBasedDictionary(string json)
        {
            _initialJson = json;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBasedDictionary"/> class.
        /// </summary>
        protected StringBasedDictionary() : this(null) {}

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        protected IDictionary<string, object> Data
        {
            get
            {
                return _data ??
                       (_data =
                        String.IsNullOrEmpty(_initialJson)
                            ? new Dictionary<string, object>()
                            : FromString(_initialJson));
            }
        }

        /// <summary>
        /// Appends a dictionary to this one.
        /// </summary>
        /// <param name="source">The source.</param>
        public void Append(IDictionary<string, object> source)
        {
            if (source == null)
            {
                return;
            }
            var target = Data;
            foreach (var key in source.Keys)
            {
                var val = source[key];
                if (val == null)
                {
                    continue;
                }
                if (target.ContainsKey(key))
                {
                    target[key] = val;
                }
                else
                {
                    target.Add(key, val);
                }
            }
        }

        /// <summary>
        /// Subtraction for dictionaries.
        /// </summary>
        /// <param name="source">The source.</param>
        public void Remove(IDictionary<string, object> source)
        {
            if (source == null)
            {
                return;
            }
            Remove(source.Keys);
        }

        /// <summary>
        /// Removes the specified keys from the dictionary.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void Remove(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                return;
            }
            var target = Data;
            foreach (var key in keys.Where(k => k != null))
            {
                if (target.ContainsKey(key))
                {
                    target.Remove(key);
                }
            }
        }

        /// <summary>
        /// Tries to get the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue(string key, out object value)
        {
            return Data.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value></value>
        public object this[string key]
        {
            get { return Data[key]; }
            set
            {
                if (string.IsNullOrEmpty(key))
                {
                    return;
                }
                var target = Data;
                if (target.ContainsKey(key))
                {
                    target[key] = value;
                }
                else
                {
                    target.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public ICollection<string> Keys
        {
            get { return Data.Keys; }
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>The values.</value>
        public ICollection<object> Values
        {
            get { return Data.Values; }
        }

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            Data.Add(key, value);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return String.IsNullOrEmpty(key) ? false : Data.Remove(key);
        }   

        #region Implementation of IEnumerable

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<string,object>>

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(KeyValuePair<string, object> item)
        {
            Data.Add(item);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Data.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return Data.Contains(item);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            Data.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, object> item)
        {
            return Data.Remove(item);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return Data.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="embedTypeInfo">if set to <c>true</c> [embed type info].</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public abstract string ToString(bool embedTypeInfo);

        /// <summary>
        /// Create a dictionary from a string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        protected abstract IDictionary<string, object> FromString(string input);
    }
}