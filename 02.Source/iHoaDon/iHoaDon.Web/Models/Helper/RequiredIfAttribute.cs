using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace iHoaDon.Web
{
    ///<summary>
    /// Required if attribute
    ///</summary>
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string _dependentProperty;
        private readonly object[] _targetValues;

        ///<summary>
        /// Ctor
        ///</summary>
        ///<param name="dependentProperty">The dependent property</param>
        ///<param name="targetValues">The target value</param>
        public RequiredIfAttribute(string dependentProperty, object[] targetValues)
        {
            _dependentProperty = dependentProperty;
            _targetValues = targetValues;
        }

        ///<summary>
        /// Ctor
        ///</summary>
        ///<param name="dependentProperty">The dependent property</param>
        ///<param name="targetValues">The target value</param>
        ///<param name="errorMessage">The error message</param>
        public RequiredIfAttribute(string dependentProperty, object[] targetValues, string errorMessage)
            : base(errorMessage)
        {
            _dependentProperty = dependentProperty;
            _targetValues = targetValues;
        }

        ///<summary>
        /// Gets dependent property
        ///</summary>
        public string DependentProperty
        {
            get { return _dependentProperty; }
        }

        ///<summary>
        /// Gets target value
        ///</summary>
        public object[] TargetValues
        {
            get { return _targetValues; }
        }

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
            dynamic propertyValue = context.ObjectType.GetProperty(DependentProperty).GetValue(context.ObjectInstance, null).ToString();

            dynamic match = TargetValues.SingleOrDefault(t => t.ToString().ToLower() == propertyValue.ToLower());

            if (match != null && value == null)
            {
                return new ValidationResult(GetErrorMessageResource());
            }

            return null;
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
                               ValidationType = "requiredif",
                               ErrorMessage = GetErrorMessageResource()
                           };

            rule.ValidationParameters["dependentproperty"] = DependentProperty;

            var first = true;
            var arrayString = new StringBuilder();

            foreach (var paramLoopVariable in TargetValues)
            {
                var param = paramLoopVariable;
                if (first)
                {
                    first = false;
                }
                else
                {
                    arrayString.Append(",");
                }
                arrayString.Append(param.ToString());
            }

            rule.ValidationParameters["targetvalues"] = arrayString.ToString();

            return new[] { rule };
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