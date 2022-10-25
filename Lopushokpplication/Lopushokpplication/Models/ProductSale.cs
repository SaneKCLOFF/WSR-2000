using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("ProductSale")]
    public partial class ProductSale
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("AgentID")]
        public int AgentId { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column(TypeName = "date")]
        public DateTime SaleDate { get; set; }
        public int ProductCount { get; set; }

        [ForeignKey("AgentId")]
        [InverseProperty("ProductSales")]
        public virtual Agent Agent { get; set; } = null!;
        [ForeignKey("ProductId")]
        [InverseProperty("ProductSales")]
        public virtual Product Product { get; set; } = null!;
    }
}
