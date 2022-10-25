using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            ProductSales = new HashSet<ProductSale>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [Column("ProductTypeID")]
        public int? ProductTypeId { get; set; }
        [StringLength(10)]
        public string ArticleNumber { get; set; } = null!;
        public string? Description { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        public int? ProductionPersonCount { get; set; }
        public int? ProductionWorkshopNumber { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MinCostForAgent { get; set; }

        [ForeignKey("ProductTypeId")]
        [InverseProperty("Products")]
        public virtual ProductType? ProductType { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
