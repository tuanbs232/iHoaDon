using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public partial class Profile
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string CompanyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Fax { get; set; }
        /// <summary>
        /// 
        /// </summary>
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
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string StartOfFinancialYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string TaxAgencyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string TaxAgencyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Issuer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Serial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime ValidTo { get; set; }

    }
}
