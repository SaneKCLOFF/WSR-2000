using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("MaterialType")]
    public partial class MaterialType
    {
        public MaterialType()
        {
            Materials = new HashSet<Material>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; } = null!;
        public double DefectedPercent { get; set; }

        [InverseProperty("MaterialType")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
