using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public partial class TemplateInvoice
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public string TemplateName { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public string IntendedUse { get; set; }

        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public string TemplateCode { get; set; }

    }
}
