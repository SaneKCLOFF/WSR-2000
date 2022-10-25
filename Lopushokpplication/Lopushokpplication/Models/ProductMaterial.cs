using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("ProductMaterial")]
    public partial class ProductMaterial
    {
        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Key]
        [Column("MaterialID")]
        public int MaterialId { get; set; }
        public double? Count { get; set; }

        [ForeignKey("MaterialId")]
        [InverseProperty("ProductMaterials")]
        public virtual Material Material { get; set; } = null!;
        [ForeignKey("ProductId")]
        [InverseProperty("ProductMaterials")]
        public virtual Product Product { get; set; } = null!;
    }
}
