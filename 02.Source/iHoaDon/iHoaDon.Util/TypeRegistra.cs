using System;
using System.Collections.Generic;

namespace iHoaDon.Util
{
    /// <summary>
    /// A registra for types. A type can register with a string and so on.
    /// This class is meant to be use from code, at design time, since instantiating a generic type at runtime can be a hassle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class TypeRegistra<T> where T : class
    {
        /// <summary>
        /// Where all registration information is kept
        /// This rely on the fact that on the CLR, TypeRegistra of T is different from TypeRegistra of X, so (TypeRegistra of T).TypeDictionary and (TypeRegistra of X).TypeDictionary are different
        /// </summary>
        private static readonly Dictionary<string, Type> TypeDictionary = new Dictionary<string, Type>();

        /// <summary>
        /// Registers the type TRegister with the string type.
        /// </summary>
        /// <typeparam name="TRegister">The type to register.</typeparam>
        /// <param name="type">The type.</param>
        public static void Register<TRegister>(string type)
        {
            if(type == null)
            {
                return;
            }
            var registrantType = typeof(TRegister);
            if (!registrantType.IsSubclassOf(typeof(T)))
            {
                throw new NotSupportedException("The specified type is not supported by this registra");
            }

            if (TypeDictionary.ContainsKey(type))
            {
                TypeDictionary[type] = registrantType;
            }
            else
            {
                TypeDictionary.Add(type, registrantType);
            }
        }

        /// <summary>
        /// Lookups and instantiate a new instance of T (who registered its type earlier).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static T Lookup(string type)
        {
            var t = LookupType(type);
            if(t != null)
            {
                return Activator.CreateInstance(t) as T;
            }
            return null;
        }

        /// <summary>
        /// Lookups the type T who previously registered with the string type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Type LookupType(string type)
        {
            if (type == null)
            {
                return null;
            }
            return TypeDictionary.ContainsKey(type) ? TypeDictionary[type] : null;
        }
    }
}