using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("MaterialCountHistory")]
    public partial class MaterialCountHistory
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("MaterialID")]
        public int MaterialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ChangeDate { get; set; }
        public double CountValue { get; set; }

        [ForeignKey("MaterialId")]
        [InverseProperty("MaterialCountHistories")]
        public virtual Material Material { get; set; } = null!;
    }
}
