using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("Supplier")]
    public partial class Supplier
    {
        public Supplier()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [Column("INN")]
        [StringLength(12)]
        [Unicode(false)]
        public string Inn { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        public int? QualityRating { get; set; }
        [StringLength(20)]
        public string? SupplierType { get; set; }

        [ForeignKey("SupplierId")]
        [InverseProperty("Suppliers")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
