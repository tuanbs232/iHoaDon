﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace iHoaDon.Web.Models.Helper
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                          Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (var name in names)
            {
                var description = name;
                var memInfo = metaData.ModelType.GetMember(name);
                if (memInfo != null)
                {
                    var attributes = memInfo[0].GetCustomAttributes(typeof (DisplayAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                        description = ((DisplayAttribute) attributes[0]).Name;
                }
                var id = string.Format("{0}_{1}_{2}", htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                                       metaData.PropertyName, name);
                var radio = htmlHelper.RadioButtonFor(expression, name, new {id = id}).ToHtmlString();
                sb.AppendFormat("<label for=\"{0}\">{1}</label> {2}", id, HttpUtility.HtmlEncode(description), radio);
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }

}