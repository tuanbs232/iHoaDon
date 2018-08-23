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
    public class InvoiceDetail
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
        [Column]
        public int LineNumber { get; set; }


        [Column]
        public string ItemCode { get; set; }


        [Column]
        public string ItemName { get; set; }


        /// <summary>
        /// Gets or sets the ip login system (client).
        /// </summary>
        /// <value>The ip of the account login.</value>
        [Column]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the time of account login system.
        /// </summary>
        /// <value>The time login.</value>
        [Column]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal ItemTotalAmountWithoutVat { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column]
        public bool Promotion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal AdjustmentVatAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public bool IsIncreaseItem { get; set; }


        [Column]
        public decimal VatAmount { get; set; }

        [Column]
        public string VatPercentage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string VatDescription { get; set; }

        [Column]
        public bool isDiscountAmtPos { get; set; }

//        [InverseProperty("InvoiceDetail")]
   //     public virtual ICollection<Invoice> Invoice { get; set; }



      
    }
}
