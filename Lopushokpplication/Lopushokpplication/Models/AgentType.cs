using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lopushokpplication.Models
{
    [Table("AgentType")]
    public partial class AgentType
    {
        public AgentType()
        {
            Agents = new HashSet<Agent>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [StringLength(100)]
        public string? Image { get; set; }

        [InverseProperty("AgentType")]
        public virtual ICollection<Agent> Agents { get; set; }
    }
}
