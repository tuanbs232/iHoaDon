using System;
using System.Xml.Serialization;

#pragma warning disable

namespace Bkav.eHoadon.XML.eHoadon.Entity.Create
{
    public partial class invoiceData
    {
        [XmlIgnore]
        public virtual bool invoiceIssuedDateSpecified
        {
            get { return invoiceIssuedDate != DateTime.MinValue; }
        }
    }
}