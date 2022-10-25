using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("Material")]
    public partial class Material
    {
        public Material()
        {
            MaterialCountHistories = new HashSet<MaterialCountHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        public int CountInPack { get; set; }
        [StringLength(10)]
        public string Unit { get; set; } = null!;
        public double? CountInStock { get; set; }
        public double MinCount { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Cost { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        [Column("MaterialTypeID")]
        public int MaterialTypeId { get; set; }

        [ForeignKey("MaterialTypeId")]
        [InverseProperty("Materials")]
        public virtual MaterialType MaterialType { get; set; } = null!;
        [InverseProperty("Material")]
        public virtual ICollection<MaterialCountHistory> MaterialCountHistories { get; set; }
        [InverseProperty("Material")]
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }

        [ForeignKey("MaterialId")]
        [InverseProperty("Materials")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
