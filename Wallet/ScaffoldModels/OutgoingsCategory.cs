using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace NorthwindEFCore.ScaffoldModels
{
    [Index(nameof(Category), IsUnique = true)]
    [Index(nameof(Id), IsUnique = true)]
    public partial class OutgoingsCategory
    {
        public OutgoingsCategory()
        {
            Outgoings = new HashSet<Outgoing>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("category", TypeName = "VARCHAR")]
        public string Category { get; set; }

        [InverseProperty(nameof(Outgoing.Category))]
        public virtual ICollection<Outgoing> Outgoings { get; set; }
    }
}
