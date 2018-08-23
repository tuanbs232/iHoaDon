using System;
using System.Collections.Generic;
using System.IO;
using RazorEngine;

namespace iHoaDon.Util
{
    /// <summary>
    /// A factory that locates and produces Sharpy Views
    /// </summary>
    public class TemplateService
    {
        /// <summary>
        /// Render the html template using the provided model and viewdata.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="output">The output.</param>
        /// <param name="model">The model.</param>
        /// <param name="viewData">The view data.</param>
        /// <param name="templateLocation">The template location.</param>
        public static void RenderFile(string template, string output, dynamic model, IDictionary<string,object> viewData = null, string templateLocation = null)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (String.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException("template");
            }
            if (String.IsNullOrEmpty(output))
            {
                throw new ArgumentNullException("output");
            }
            if (!File.Exists(template))
            {
                throw new Exception("Template Not Found!");
            }
            var templateContent = File.ReadAllText(template);
            var result = Razor.Parse(templateContent, model);
            File.WriteAllText(output, result);
        }

        /// <summary>
        /// Renders the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns></returns>
        public static string RenderLiteral(string template, dynamic model, IDictionary<string, object> viewData = null)
        {
            string result;
            if (String.IsNullOrEmpty(template))
            {
                result = String.Empty;
            }
            else
            {
                if (!File.Exists(template))
                {
                    throw new Exception("Template Not Found!");
                }
                var templateContent = File.ReadAllText(template);
                result = Razor.Parse(templateContent, model);
            }
            return result;
        }
    }
}