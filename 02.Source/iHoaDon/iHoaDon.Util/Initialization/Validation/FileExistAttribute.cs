using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace iHoaDon.Util
{
    /// <summary>
    /// Check if the file exist
    /// </summary>
    public class FileExistAttribute:ValidationAttribute
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
            if(value == null || Equals(value, String.Empty))
            {
                return false;
            }
            return File.Exists(value.ToString());
        }

        #endregion
    }
}