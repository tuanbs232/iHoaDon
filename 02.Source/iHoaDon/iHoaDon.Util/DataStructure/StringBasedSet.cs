using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// A set with a string as backend
    /// </summary>
    public class StringBasedSet:ICollection<string>
    {
        private const string Delimiters = ",;| ";

        #region private
        private readonly char[] _delimiters;

        private readonly string _initial;

        private HashSet<string> _list; 
        #endregion

        /// <summary>
        /// Assign something here in order to get the result of the saving process
        /// </summary>
        public Action<string> UpdateModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBasedSet"/> class.
        /// </summary>
        /// <param name="initial">The initial.</param>
        /// <param name="delimiters">The delimiters.</param>
        public StringBasedSet(string initial, char[] delimiters)
        {
            _initial = initial;
            _delimiters = delimiters == null || delimiters.Length == 0 ? Delimiters.ToCharArray() : delimiters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBasedSet"/> class.
        /// </summary>
        public StringBasedSet():this(null,null){}

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <value>The elements.</value>
        private ICollection<string> Elements
        {
            get
            {
                return _list ?? (_list =
                                 String.IsNullOrEmpty(_initial)
                                     ? new HashSet<string>()
                                     : new HashSet<string>(
                                           _initial.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries))
                                );
            }
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return Elements.GetEnumerator();
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

        #region Implementation of ICollection<string>

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(string item)
        {
            if (String.IsNullOrEmpty(item))
            {
                return;
            }
            Elements.Add(item);
            TryUpdateModel();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Elements.Clear();
            TryUpdateModel();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string item)
        {
            return Elements.Contains(item);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(string[] array, int arrayIndex)
        {
            Elements.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(string item)
        {
            var isRemoved = Elements.Remove(item);
            if(isRemoved)
            {
                TryUpdateModel();
            }
            return isRemoved;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return Elements.Count; }
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
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(char delimiter)
        {
            return String.Join(delimiter.ToString(), Elements.ToArray());
        }

        /// <summary>
        /// Tries to update the source (backend).
        /// </summary>
        private void TryUpdateModel()
        {
            if (UpdateModel != null)
            {
                UpdateModel(ToString(_delimiters[0]));
            }
        }
    }
}