using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public partial class ListReleaseInvoice
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int Quantity { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int StartNumber { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int EndNumber { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public DateTime StartUsingDate { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int AccountId { get; set; }
        
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public string TemplateCode { get; set; }

        [NotMapped]
        public string TemplateName { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int Status { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public string SerialInvoice { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public string No { get; set; }
    }
}
