using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using iHoaDon.Entities;

namespace iHoaDon.Entities
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public string ProductName { get; set; }
        /// <summary>
        /// gets or sets the password
        /// </summary>
        [Column]
        public int? UnitID { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public decimal? Price { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int? CateID { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public decimal? Discount { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public string Note { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int? AccountId { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public int? StoreID { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public bool IsExprise { get; set; }
        /// <summary>
        /// gets or sets the LoginName
        /// </summary>
        [Column]
        public string ProductCode { get; set; }

      public virtual Unit Unit { get; set; }
    }
}
