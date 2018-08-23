using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Bkav.eHoadon.XML.eHoadon.Entity.Message;

namespace Bkav.eHoadon.XML.xsd
{
    public class XsdValidator
    {
        public static string IsValidInvoiceCreate(string xmlString)
        {
            string[] xsdContents = { Resource.commons, Resource.invoice_create, Resource.signature };

            string isValidInvoiceCreate = IsValidXml(xmlString, xsdContents);

            if (!String.IsNullOrEmpty(isValidInvoiceCreate))
                throw new Exception(isValidInvoiceCreate);

            return isValidInvoiceCreate;
        }

        public static string IsValidInvoiceMessage(string xmlString)
        {
            var transaction = new transaction();
            transaction.signatureSpecified = false;
            string trans = transaction.Serialize(Encoding.UTF8);
            trans = trans.Replace("</inv:transaction>", "<inv:invoices></inv:invoices></inv:transaction>");
            if (xmlString != null)
            {
                xmlString = xmlString.Substring(xmlString.IndexOf("<inv:invoice"));
                trans = trans.Replace("</inv:invoices>", xmlString + "</inv:invoices>");
            }
            else
                return null;

            return IsValidTransactionMessage(trans);
        }

        public static string IsValidTransactionMessage(string xmlString)
        {
            string[] xsdContents = { Resource.commons, Resource.invoice_message, Resource.signature, Resource.errors };

            string isValidTransactionMessage = IsValidXml(xmlString, xsdContents);

            if (!String.IsNullOrEmpty(isValidTransactionMessage))
                throw new Exception(isValidTransactionMessage);

            return isValidTransactionMessage;
        }

        public static string IsValidXml(string xmlString, string[] xsdContents)
        {
            if (String.IsNullOrEmpty(xmlString))
            {
                throw new ArgumentNullException("xmlString is null");
            }

            if (xsdContents == null || xsdContents.Length == 0)
            {
                throw new ArgumentNullException("xsdContents is null");
            }

            try
            {
                var settings = new XmlReaderSettings();

                XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
                foreach (var xsdContent in xsdContents)
                {
                    xmlSchemaSet.Add(null, XmlReader.Create(new StringReader(xsdContent)));
                }

                settings.Schemas.Add(xmlSchemaSet);
                settings.ValidationType = ValidationType.Schema;

                using (var reader = XmlReader.Create(new StringReader(xmlString), settings))
                {
                    while (reader.Read()) { }
                }

                return String.Empty;
            }

            catch (Exception e)
            {
                if (e.InnerException == null)
                    return e.Message;

                return e.Message + " "
                       + e.InnerException.Message;
            }
        }
    }
}