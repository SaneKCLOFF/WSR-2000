using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("ProductCostHistory")]
    public partial class ProductCostHistory
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ChangeDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal CostValue { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductCostHistories")]
        public virtual Product Product { get; set; } = null!;
    }
}
