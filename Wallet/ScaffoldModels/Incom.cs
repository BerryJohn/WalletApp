using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace NorthwindEFCore.ScaffoldModels
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(UserId), IsUnique = true)]
    public partial class Incom
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("userId")]
        public long UserId { get; set; }
        [Column("income")]
        public long Income { get; set; }
        [Column("categoryId")]
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(IncomeCategory.Incoms))]
        public virtual IncomeCategory Category { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Incom")]
        public virtual User User { get; set; }
    }
}
