using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public partial class InvoiceNumber
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int InvoicesNumber { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int UseStatus { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int ReleaseId { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int Status { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int AccountId { get; set; }
    }
}
