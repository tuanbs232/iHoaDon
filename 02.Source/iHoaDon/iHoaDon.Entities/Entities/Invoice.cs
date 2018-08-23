using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    /// <summary>
    /// An Account Log
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the login name.
        /// </summary>
        /// <value>The name of the login.</value>
        [Required]
        public string InvoiceNote { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public decimal TotalAmountWithoutVAT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public decimal DiscountAmount { get; set; }


        /// <summary>
        /// Gets or sets the ip login system (client).
        /// </summary>
        /// <value>The ip of the account login.</value>
        [Required]
        public decimal TotalAmountWithVAT { get; set; }

        /// <summary>
        /// Gets or sets the time of account login system.
        /// </summary>
        /// <value>The time login.</value>
        [Column]
        public string AdjustmentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime InvoiceIssuedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Serial { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string TemplateCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string InvoiceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int InvoiceDetailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal TotalVATAmount { get; set; }


        [Column]
        public int DeliveryID { get; set; }

        [Column]
        public int CurrencyID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int CustomerInvoiceID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual CustomerInvoice CustomerInvoice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public virtual InvoiceDetail InvoiceDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Currency Currency { get; set; }

    }
}
