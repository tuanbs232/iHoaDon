using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace iHoaDon.Util
{
    /// <summary>
    /// Check if the directory exist
    /// </summary>
    public class DirectoryExistAttribute:ValidationAttribute
    {
        #region Overrides of ValidationAttribute

        /// <summary>
        /// Determines whether the specified value of the object is valid. 
        /// </summary>
        /// <returns>
        /// true if the specified value is valid; otherwise, false. 
        /// </returns>
        /// <param name="value">The value of the specified validation object on which the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/> is declared.
        ///                 </param>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            var path = value.ToString();
            if (String.IsNullOrEmpty(path))
            {
                return false;
            }

            var dirPath = Path.GetDirectoryName(path);

            return !String.IsNullOrEmpty(dirPath) && Directory.Exists(dirPath);
        }

        #endregion
    }
}