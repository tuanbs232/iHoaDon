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
    public class CustomerInvoice
    {
        #region Members
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
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the ip login system (client).
        /// </summary>
        /// <value>The ip of the account login.</value>
        [Required]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the time of account login system.
        /// </summary>
        /// <value>The time login.</value>
        [Column]
        public string CompanyCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Address { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string BankAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string BankName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string BuyerEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int AccountId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual Account Account { get; set; }

        #endregion
    }
}
