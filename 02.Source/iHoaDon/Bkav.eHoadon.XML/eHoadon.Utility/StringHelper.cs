using System;
using System.Xml;
using Bkav.eHoadon.XML.eHoadon.Entity.Message;
using Bkav.eHoadon.XML.eHoadon.Signning;

namespace Bkav.eHoadon.XML.eHoadon.Utility
{
    public class StringHelper
    {
        /// <summary>
        /// Lay ra du lieu giua 2 the
        /// </summary>
        /// <param name="str">Xau dau vao</param>
        /// <param name="from">The mo</param>
        /// <param name="to">The dong</param>
        /// <returns></returns>
        public static String GetSubStringBetween(string str, string from, string to)
        {
            if (str == null || @from == null || to == null)
                return null;

            int startIndex = str.IndexOf(@from) + @from.Length;
            int endIndex = str.IndexOf(to);
            return str.Substring(startIndex, endIndex - startIndex);
        }

        public static String BuildErrorMsg(String code, String description, String invAsString)
        {
            var errorMsg = "<inv:error><inv:code>" + code + "</inv:code><inv:description>" + description + "</inv:description></inv:error>";

            if (invAsString != null && invAsString.Contains("</inv:invoice>"))
                return invAsString.Replace("</inv:invoice>", errorMsg + "</inv:invoice>");
            return errorMsg;
        }

        public static void ExtractInvoiceFromResult(string certfiedTrans)
        {
            if (String.IsNullOrEmpty(certfiedTrans))
            {
                Console.WriteLine("Khong co ket qua");
                return;
            }

            // kiem tra xem ca transaction co bi loi ko:

            transaction trans = EntityBase<transaction>.Deserialize(certfiedTrans);

            error errorTrans = trans.error;
            if (errorTrans != null && !String.IsNullOrEmpty(errorTrans.code))
            {
                Console.WriteLine("Transaction error: " + ":" + errorTrans.code + ":" + errorTrans.description);
                Console.WriteLine("------------------------------------------------------------------");
                return;
            }

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(certfiedTrans);

            XmlElement xmlElement = xmlDocument["inv:transaction"]["inv:invoices"];

            XmlNodeList invNodeList = xmlElement.ChildNodes;

            foreach (XmlNode invNode in invNodeList)
            {
                // loai di namespace thua o cac the 
                string invoiceData = invNode.InnerXml;
                invoiceData = invoiceData.Replace(" xmlns:inv=\"http://laphoadon.gdt.gov.vn/2014/09/invoicexml/v1\"", String.Empty);

                string finalInvoice = "<inv:invoice xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:inv=\"http://laphoadon.gdt.gov.vn/2014/09/invoicexml/v1\">" +
                                      invoiceData + "</inv:invoice>";

                Bkav.eHoadon.XML.eHoadon.Entity.Message.invoice inv = EntityBase<Bkav.eHoadon.XML.eHoadon.Entity.Message.invoice>.Deserialize(finalInvoice);

                // hoa don xac thuc thanh cong
                if (inv.error == null || inv.error.Count == 0)
                {
                    // Kiem tra chu ky dien tu:
                    Console.WriteLine("Kiem tra chư ky dien tu:" + Bkav.eHoadon.XML.eHoadon.Signning.eHoadonSign.EnvelopedXmlVerify(finalInvoice));

                    Console.WriteLine("-------------------------Hoa don can luu--------------------------");
                    Console.WriteLine(finalInvoice);
                    Console.WriteLine("------------------------------------------------------------------");

                    Console.WriteLine(inv.invoiceData.invoiceAppRecordId + ":success:" + inv.certifiedData.certifiedId);
                }
                // hoa don xac thuc bi loi
                else
                {
                    Console.WriteLine("-------------------------Hoa don bi loi--------------------------");
                    foreach (error error in inv.error)
                    {
                        Console.WriteLine(inv.invoiceData.invoiceAppRecordId + ":" + error.code + ":" + error.description);
                    }
                    Console.WriteLine("------------------------------------------------------------------");

                }
            }

        }
    }
}