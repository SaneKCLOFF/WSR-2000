using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("Shop")]
    public partial class Shop
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [StringLength(300)]
        public string? Address { get; set; }
        [Column("AgentID")]
        public int AgentId { get; set; }

        [ForeignKey("AgentId")]
        [InverseProperty("Shops")]
        public virtual Agent Agent { get; set; } = null!;
    }
}
