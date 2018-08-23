using System.Collections.Generic;
using System.Diagnostics;
namespace iHoaDon.Entities
{
    /// <summary>
    /// mail information class
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Email class
        /// </summary>
        [DebuggerStepThrough]
        public Email()
        {
            To = new List<string>();
            ReplyTo = new List<string>();
            CC = new List<string>();
            Bcc = new List<string>();
            Headers = new Dictionary<string, string>();
        }
        /// <summary>
        /// From email
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Send to
        /// </summary>
        public ICollection<string> To { get; private set; }
        /// <summary>
        /// Reply to
        /// </summary>
        public ICollection<string> ReplyTo { get; private set; }
        /// <summary>
        /// CC
        /// </summary>
        public ICollection<string> CC { get; private set; }
        /// <summary>
        /// BCC
        /// </summary>
        public ICollection<string> Bcc { get; private set; }
        /// <summary>
        /// Mail header
        /// </summary>
        public IDictionary<string, string> Headers { get; private set; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Body contents with html temp
        /// </summary>
        public string HtmlBody { get; set; }
        /// <summary>
        /// body contents with text
        /// </summary>
        public string TextBody { get; set; }
        
    }
}
