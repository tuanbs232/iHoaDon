using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace iHoaDon.Web
{
    ///<summary>
    /// Tax number attribute
    ///</summary>
    public class TaxNumberNewAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class. 
        /// </returns>
        /// <param name="value">The value to validate.</param>
        /// <param name="context"></param>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value.ToString())) return null;
            var match = CheckTaxNumberFormat(value.ToString());

            return !match ? new ValidationResult(GetErrorMessageResource()) : null;
        }

        /// <summary>
        /// When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        /// <param name="metadata">The model metadata.</param><param name="context">The controller context.</param>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "taxnumber",
                ErrorMessage = GetErrorMessageResource()
            };

            rule.ValidationParameters["propertyname"] = metadata.PropertyName;

            return new[] { rule };
        }

        private static bool CheckTaxNumberFormat(string taxNumber)
        {
            //string msg = "Mã số thuế sai định dạng, vui lòng nhập lại";
            if (taxNumber.Length > 14 || taxNumber.Length < 10 || (taxNumber.Length > 10 && taxNumber.Length < 13))
            {
                return false;
            }
            if (taxNumber.Length == 10)
            {
                if (IsNumeric(taxNumber))
                {
                    if (!CheckString10Number(taxNumber))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (taxNumber.Length == 13)
            {
                if (IsNumeric(taxNumber))
                {
                    var n1To10 = taxNumber.Substring(0, 10);
                    if (!CheckString10Number(n1To10))
                    {
                        return false;
                    }
                    var n11 = int.Parse(taxNumber.Substring(10, 1));
                    var n12 = int.Parse(taxNumber.Substring(11, 1));
                    var n13 = int.Parse(taxNumber.Substring(12, 1));
                    if (!(n11 >= 0 || n11 <= 9) || !(n12 >= 0 || n12 <= 9) || !(n13 >= 0 || n13 <= 9))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (taxNumber.Length == 14)
            {
                var n1To10 = taxNumber.Substring(0, 10);
                var n12To14 = taxNumber.Substring(11, 3);
                var n11 = taxNumber.Substring(10, 1);
                if (n11.Equals("-"))
                {
                    if (IsNumeric(n1To10) && IsNumeric(n12To14))
                    {
                        var n12 = int.Parse(taxNumber.Substring(11, 1));
                        var n13 = int.Parse(taxNumber.Substring(12, 1));
                        var n14 = int.Parse(taxNumber.Substring(13, 1));
                        if (!CheckString10Number(n1To10))
                        {
                            return false;
                        }
                        if (!(n12 >= 0 || n12 <= 9) || !(n13 >= 0 || n13 <= 9) || !(n14 >= 0 || n14 <= 9))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckString10Number(string str)
        {
            var n1 = (int.Parse(str.Substring(0, 1))) * 31;
            var n2 = (int.Parse(str.Substring(1, 1))) * 29;
            var n3 = (int.Parse(str.Substring(2, 1))) * 23;
            var n4 = (int.Parse(str.Substring(3, 1))) * 19;
            var n5 = (int.Parse(str.Substring(4, 1))) * 17;
            var n6 = (int.Parse(str.Substring(5, 1))) * 13;
            var n7 = (int.Parse(str.Substring(6, 1))) * 7;
            var n8 = (int.Parse(str.Substring(7, 1))) * 5;
            var n9 = (int.Parse(str.Substring(8, 1))) * 3;
            var n10 = (int.Parse(str.Substring(9, 1)));
            var remainder = (n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9) % 11;
            return 10 - remainder == n10;
        }

        private static bool IsNumeric(string strVal)
        {
            var reg = new Regex("[^0-9-]");
            var reg2 = new Regex("^-[0-9]+$|^[0-9]+$");
            return (!reg.IsMatch(strVal) && reg2.IsMatch(strVal));
        }

        private string GetErrorMessageResource()
        {
            var errorMessageRes = ErrorMessageResourceType != null && ErrorMessageResourceName != null 
                                ? ErrorMessageResourceType.GetProperty(ErrorMessageResourceName)
                                : null;
            return errorMessageRes != null
                       ? errorMessageRes.GetValue(errorMessageRes.DeclaringType, null).ToString()
                       : ErrorMessage;
        }
    }
}