using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Fasterflect;
namespace iHoaDon.Util
{
    /// <summary>
    /// This class offers initialization services to other classes.
    /// To benefit from this service, a class needs to decorate its properties with some attributes (ConfigField, PreProcessor, PostProcessor)
    /// </summary>
    public static class InitializationHelper
    {
        #region Initialization service
        /// <summary>
        /// Initializes the specified component (by default, it uses app.config).
        /// </summary>
        /// <param name="component">The component.</param>
        public static void Initialize(this object component)
        {
            Initialize(component, k => ConfigurationManager.AppSettings.Get(k));
        }

        /// <summary>
        /// Initializes the specified component using a dictionary of settings.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="config">The config.</param>
        public static void Initialize(this object component, IDictionary<string, string> config)
        {
            if (config == null)
            {
                Initialize(component);
            }
            else
            {
                Initialize(component, k => config.ContainsKey(k) ? config[k] : null);
            }
        }

        /// <summary>
        /// Initializes the specified component using the specified resolver.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="resolver">The resolver.</param>
        public static void Initialize(this object component, Func<string, string> resolver)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }


            foreach (var prop in component.GetType()
                                            .Properties()
                                            .Where(p=>p.HasAttribute<ConfigFieldAttribute>()))
            {
                var configFieldGetter = prop.Attribute<ConfigFieldAttribute>();
                var valueString = configFieldGetter.GetStringValue(resolver);
                valueString = PreProcess(prop, valueString);
                var value = PostProcess(prop, valueString);
                ValidateValue(prop, value);
                prop.Set(component, value);
            }
        }
        #endregion

        #region Initialization Steps

        /// <summary>
        /// Run all the validation attributes.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="value">The value.</param>
        private static void ValidateValue(MemberInfo prop, object value)
        {
            foreach (var validator in prop.Attributes<ValidationAttribute>())
            {
                if (!validator.IsValid(value))
                {
                    throw new ValidationException(validator.FormatErrorMessage(prop.Name));
                }
            }
        }

        /// <summary>
        /// Runs all preprocessor in chains, piping the output of this one with the next.
        /// The preprocessor with higher priority will run first
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="valueString">The value string.</param>
        /// <returns></returns>
        private static string PreProcess(MemberInfo prop, string valueString)
        {
            var preProcessors = prop.Attributes<PreRetrievalProcessingAttribute>()
                                    .OrderBy(p=>p.Priority);

            valueString = preProcessors.Aggregate
            (
                valueString, (current, preProcessor) =>
                {
                    try
                    {
                        return preProcessor.Process(current);
                    }
                    catch(Exception)
                    {
                        if(!preProcessor.SuppressException)
                        {
                            throw;
                        }
                        return current;
                    }
                }
            );
            return valueString;
        }

        /// <summary>
        /// If there is a post processor, run it, else use regular typecast
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="valueString">The value string.</param>
        /// <returns></returns>
        private static object PostProcess(MemberInfo prop, string valueString)
        {
            var postProcessors = prop.Attribute<PostRetrievalProcessingAttribute>();
            return postProcessors != null 
                       ? postProcessors.Process(valueString, prop.Type()) 
                       : CastValue(prop.Type(), valueString);
        }

        /// <summary>
        /// Casts the value string into the target type, short-circuit if the target type is also typeof(string).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static object CastValue(Type type, string value)
        {
            object result;

            if (type != typeof(string))
            {
                try
                {
                    var converter = TypeDescriptor.GetConverter(type);
                    if (converter == null)
                    {
                        throw new Exception("No converter found for type" + type.Name);
                    }
                    result = converter.ConvertFromString(value);
                }
                catch(Exception exception)
                {
                    throw new Exception(String.Format("Could not cast {0} to type {1}:{2}", value ?? "NULL", type.Name, exception.Message));
                }
            }
            else
            {
                result = value;
            }

            return result;
        }

        #endregion 
    }
}