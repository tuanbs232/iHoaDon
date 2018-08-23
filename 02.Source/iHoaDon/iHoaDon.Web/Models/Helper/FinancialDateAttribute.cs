using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace iHoaDon.Web
{
    ///<summary>
    /// Vietnamese datetime attribuite
    ///</summary>
    public class FinancialDateAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class. 
        /// </returns>
        /// <param name="value">The value to validate.</param><param name="validationContext">The context information about the validation operation.</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = null;
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if (value.ToString().Length < 5)
                {
                    result = new ValidationResult(GetErrorMessageResource());
                }
                else
                {
                    DateTime date;
                    var valid = DateTime.TryParse(value + "/2000",
                                                CultureInfo.GetCultureInfo("vi-VN").DateTimeFormat,
                                                DateTimeStyles.None,
                                                out date);
                    if (!valid)
                    {
                        result = new ValidationResult(GetErrorMessageResource());
                    }
                }
            }
            
            return result;
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
                ValidationType = "financialdate",
                ErrorMessage = GetErrorMessageResource()
            };

            rule.ValidationParameters["propertyname"] = metadata.PropertyName;

            return new[] { rule };
        }
    }
}
