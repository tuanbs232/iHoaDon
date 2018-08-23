using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;

namespace iHoaDon.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public string Index()
        {
            string result = string.Empty;
            var mappath = Server.MapPath("~/Content/Viewer");
            string xmlContent = System.IO.File.ReadAllText(string.Format("{0}\\InvoiceResult.xml", mappath));
            string xsltContent = System.IO.File.ReadAllText(string.Format("{0}\\01GTKT.xslt", mappath));
            try
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                XsltSettings settings = new XsltSettings(true, true);
                using (XmlReader reader = XmlReader.Create(new StringReader(xsltContent)))
                {
                    transform.Load(reader, settings, new XmlUrlResolver());
                }
                StringWriter results = new StringWriter();
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent)))
                {
                    transform.Transform(reader, null, results);
                }
                result = string.IsNullOrEmpty(results.ToString()) ? xsltContent : results.ToString();
            }
            catch (Exception exception)
            {

                System.Diagnostics.Debug.WriteLine(exception);
            }
            return result;
        }

    }
}
