using System.Xml;
using System.Xml.Linq;

namespace iHoaDon.Util
{
    /// <summary>
    /// Bridge between System.Xml.XmlDocument and System.Xml.Linq.XDocument
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Convert from XDoc to XmlDoc.
        /// </summary>
        /// <param name="xDocument">The x document.</param>
        /// <returns></returns>
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        /// <summary>
        /// Convert from XmlDoc to XDoc.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns></returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
    }
}