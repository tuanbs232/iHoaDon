using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities.Entities
{
    /// <summary>
    /// thông tin tỷ giá
    /// </summary>
    public class TaxRate
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Giá trị tỷ giá
        /// </summary>
        [Column]
        public string TaxRateValue { get; set; }
        /// <summary>
        /// Tên tỷ giá
        /// </summary>
        [Column]
        public string TaxRateName { get; set; }
    }
}
