using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public DateTime CreateDate { get; set; }
        
        [Column]
        public DateTime DateModify { get; set; }

        [Column]
        public int AccountID { get; set; }

        [Column]
        public string InvoiceType { get; set; }
        [Column]
        public string TemplateCode { get; set; }
        [Column]
        public string InvoiceSeries { get; set; }
        [Column]
        public int InvoiceID { get; set; }
        [Column]
        public string InvoiceXML { get; set; }
        [Column]
        public string InvoiceMessage { get; set; }
        [Column]
        public string ResultID { get; set; }
        [Column]
        public string ResultSummary { get; set; }
        [Column]
        public string InvoiceXMLSign { get; set; }
        [Column]
        public string InvoiceResult { get; set; }
        [Column]
        public dynamic TotalAmount { get; set; }
        [Column]
        public string CertifiedID { get; set; }

    }
}
