using System;
using System.Linq;
using System.Reflection;

namespace iHoaDon.Util
{
    /// <summary>
    /// Initiate an object using its class name
    /// </summary>
    public class InitiateFromClassNameAttribute : PostRetrievalProcessingAttribute
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public override object Process(string input, Type targetType)
        {
            var type = Assembly.GetAssembly(targetType).GetType(input, false, true) ?? Assembly.GetExecutingAssembly().GetType(input, false, true);
            if(type == null)
            {
                throw new Exception("Type not found");
            }
            if(!(type.IsSubclassOf(targetType) || type.GetInterfaces().Contains(targetType)))
            {
                throw new Exception("Config type does not match target type");
            }
            return Activator.CreateInstance(type);
        }
    }
}