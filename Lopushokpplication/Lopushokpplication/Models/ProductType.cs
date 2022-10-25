using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("ProductType")]
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; } = null!;
        public double DefectedPercent { get; set; }

        [InverseProperty("ProductType")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
