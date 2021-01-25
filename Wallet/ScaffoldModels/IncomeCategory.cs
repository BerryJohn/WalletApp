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
    public partial class IncomeCategory
    {
        public IncomeCategory()
        {
            Incoms = new HashSet<Incom>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("category", TypeName = "VARCHAR")]
        public string Category { get; set; }

        [InverseProperty(nameof(Incom.Category))]
        public virtual ICollection<Incom> Incoms { get; set; }
    }
}
