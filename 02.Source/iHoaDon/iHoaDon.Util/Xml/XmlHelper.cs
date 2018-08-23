using System;
using System.IO;
using System.Xml.Serialization;

namespace iHoaDon.Util
{
    /// <summary>
    /// Read/write object to/from xml using System.Xml.Serialization
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Write an object graph to a xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath">The XML path.</param>
        /// <param name="graph">The graph.</param>
        /// <param name="types">The types.</param>
        public static void ToXml<T>(string xmlPath, T graph, params Type[] types)
        {
            using (var fs = File.OpenWrite(xmlPath))
            {
                var serializer = new XmlSerializer(typeof(T), types);
                serializer.Serialize(fs, graph);
            }
        }

        /// <summary>
        /// Write the object graph to xml string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public static string ToXml<T>(this T graph, params Type[] types)
        {
            using (var writer = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T), types);
                serializer.Serialize(writer, graph);
                writer.Flush();
                return writer.ToString();
            }
        }

        /// <summary>
        /// Load an object graph from an xml file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath">The XML path.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public static T FromXml<T>(string xmlPath, params Type[] types) where T : class
        {
            if (String.IsNullOrEmpty(xmlPath))
            {
                throw new ArgumentNullException("xmlPath");
            }
            if (!File.Exists(xmlPath))
            {
                throw new FileNotFoundException("Form definition not found: " + xmlPath);
            }
            using (var fs = File.OpenRead(xmlPath))
            {
                return FromXml<T>(fs, types);
            }
        }

        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public static T FromXml<T>(Stream input, params Type[] types) where T : class
        {
            var serializer = new XmlSerializer(typeof(T), types);
            return serializer.Deserialize(input) as T;
        }

        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static T FromXml<T>(TextReader reader) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(reader) as T;
        }

        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static T FromXml<T>(Stream input) where T : class
        {
            return FromXml<T>(input);
        }
    }
}
