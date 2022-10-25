using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("Agent")]
    public partial class Agent
    {
        public Agent()
        {
            AgentPriorityHistories = new HashSet<AgentPriorityHistory>();
            ProductSales = new HashSet<ProductSale>();
            Shops = new HashSet<Shop>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [Column("AgentTypeID")]
        public int AgentTypeId { get; set; }
        [StringLength(300)]
        public string? Address { get; set; }
        [Column("INN")]
        [StringLength(12)]
        [Unicode(false)]
        public string Inn { get; set; } = null!;
        [Column("KPP")]
        [StringLength(9)]
        [Unicode(false)]
        public string? Kpp { get; set; }
        [StringLength(100)]
        public string? DirectorName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; } = null!;
        [StringLength(255)]
        public string? Email { get; set; }
        [StringLength(100)]
        public string? Logo { get; set; }
        public int Priority { get; set; }

        [ForeignKey("AgentTypeId")]
        [InverseProperty("Agents")]
        public virtual AgentType AgentType { get; set; } = null!;
        [InverseProperty("Agent")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistories { get; set; }
        [InverseProperty("Agent")]
        public virtual ICollection<ProductSale> ProductSales { get; set; }
        [InverseProperty("Agent")]
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
