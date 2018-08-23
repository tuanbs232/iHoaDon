using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace iHoaDon.Entities
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [InverseProperty("Unit")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
