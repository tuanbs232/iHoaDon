using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace Bkav.eHoadon.XML.eHoadon.Signning
{
    /// <author>
    /// Bkav Corp. - [Department]
    /// Project: [Project Name]
    /// Create Date : [03/10/2014]
    /// Author      : [HuyND] 
    /// Description : ...
    /// ************************************************* 
    /// - [HuyND@bkav.com] - [03/10/2014]: [Lý do sửa ...]
    /// </author>
    /// <summary>
    /// Class bao gồm các hàm ký xml trả về chuỗi xml đã ký số, verify chữ ký số trên xml
    /// </summary>
    public class eHoadonSign
    {
        /// <summary>
        /// Hàm verify chữ ký số dạng XML
        /// </summary>
        /// <param name="xmldoc">Xâu XML được gắn chữ ký số theo chuẩn</param>
        /// <returns></returns>
        public static bool EnvelopedXmlVerify(string xmldoc)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmldoc);

            return EnvelopedXmlVerify(xmlDocument);
        }


        /// <summary>
        /// Hàm verify chữ ký số dạng XML
        /// </summary>
        /// <param name="xmldoc">Tài liệu XML được gắn chữ ký số theo chuẩn</param>
        /// <returns>True/False</returns>
        public static bool EnvelopedXmlVerify(XmlDocument xmldoc)
        {
            if (xmldoc == null)
                return false;
            SignedXml sxml = new SignedXml(xmldoc);
            // Get the XML Signature node and load it into the signed XML object.
            XmlNodeList list = xmldoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl);

            if (list.Count == 0)
                return false;

            foreach (XmlNode xmlNode in list)
            {
                string value = xmlNode.Attributes["Id"].Value;

                if (value == "seller")
                {
                    sxml.LoadXml((XmlElement)xmlNode);
                    break;
                }
            }

            // Verify the signature.
            return sxml.CheckSignature();
        }

        /// <summary>
        /// Đây là ví dụ ký một Thẻ, chúng ta có thể ký nhiều thẻ bằng cách tạo nhiều Reference
        /// SignedXml signer = new SignedXml(doc);
        /// signer.AddReference(new Reference("#tag1"));
        /// signer.AddReference(new Reference("#tag3"));
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="cert">Certificate này phải chứa Private Key</param>
        /// <param name="uri">chứa URI tới dữ liệu cần ký: nếu Uri="" tức là ký toàn bộ tài liệu</param>
        /// <returns></returns>
        public static string Sign(XmlDocument xmldoc, X509Certificate2 cert, string uri)
        {
            const bool c14 = true;
            if (xmldoc == null || cert == null)
                return null;
            if (String.IsNullOrEmpty(uri))
                throw new Exception("Chưa cung cấp thông tin id thẻ invoiceData");

            if (!uri.StartsWith("#"))
                uri = "#" + uri;

            if (!cert.HasPrivateKey)
                throw new Exception("Chứng thư không có khóa bí mật");

            // Creating the XML signing object.
            SignedXml sxml = new SignedXml(xmldoc);

            KeyInfo keyInfo = new KeyInfo();
            // Load the certificate into a KeyInfoX509Data object
            // and add it to the KeyInfo object.
            keyInfo.AddClause(new KeyInfoX509Data(cert));

            // Add the KeyInfo object to the SignedXml object.
            sxml.KeyInfo = keyInfo;

            sxml.SigningKey = cert.PrivateKey;

            // Set the canonicalization method for the document.
            sxml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigCanonicalizationUrl; // No comments.

            ////============================Add SignatureProperties===========================//

            // Create an empty reference (not enveloped) for the XPath
            // transformation.
            Reference r = new Reference(uri);

            // Create the XPath transform and add it to the reference list.
            r.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            if (c14)
            {
                XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
                r.AddTransform(c14t);
            }

            Reference reference = new Reference("#AMadeUpTimeStamp");

            // Create the XPath transform and add it to the reference list.
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            if (c14)
            {
                XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
                reference.AddTransform(c14t);
            }

            ////============================Add SignatureProperties===========================//

            // Add the reference to the SignedXml object.
            sxml.AddReference(r);
            sxml.Signature.Id = "seller";
            // Compute the signature.
            sxml.ComputeSignature();

            // Get the signature XML and add it to the document element.
            XmlElement sig = sxml.GetXml();
            xmldoc.DocumentElement.AppendChild(sig);

            return xmldoc.OuterXml;
        }

    }
}
