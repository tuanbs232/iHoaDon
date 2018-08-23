using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace iHoaDon.Web.Helper
{
    public static class DropDownListForEnumHelper
    {
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue));
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, object htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), htmlAttributes);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, IDictionary<string, object> htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), htmlAttributes);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string optionLabel, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), optionLabel);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string optionLabel, IDictionary<string, object> htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), optionLabel, htmlAttributes);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Type enumType, string optionLabel, object htmlAttributes, string selectedValue = "")
        {
            return htmlHelper.DropDownListFor(expression, GetListEnum(enumType, selectedValue), optionLabel, htmlAttributes);
        }

        private static IEnumerable<SelectListItem> GetListEnum(Type enumeration, string selectedValue)
        {
            if (!enumeration.IsEnum)
            {
                throw new ArgumentException(@"passed type must be of Enum type", "enumeration");
            }
            return Enum.GetNames(enumeration)
                        .Select(member=>new SelectListItem
                        {
                            Value = member,
                            Text = RetrieveDescription(member, enumeration) ?? member,
                            Selected = selectedValue == member
                        });
        }

        public static String RetrieveDescription(string name, Type enumeration)
        {
            var mi = enumeration.GetMember(name);
            if (mi.Length > 0)
            {
                var attributes = mi[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return null;
        }
    }
}