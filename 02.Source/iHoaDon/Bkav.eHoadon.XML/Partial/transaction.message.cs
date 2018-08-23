using System;

namespace Bkav.eHoadon.XML.eHoadon.Entity.Message
{
    public partial class transaction
    {
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool signatureSpecified
        {
            get
            {
                return this.Signature != null;
            }
            set
            {
                if (value == false)
                {
                    this.Signature = null;
                }
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool errorSpecified
        {
            get
            {
                return this.error != null && !String.IsNullOrEmpty(error.code);
            }
            set
            {
                if (value == false)
                {
                    this.error = null;
                }
            }
        }
    }
}
