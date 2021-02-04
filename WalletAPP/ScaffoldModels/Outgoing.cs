using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WalletDB.ScaffoldModels
{
    [Index(nameof(Id), IsUnique = true)]
    public partial class Outgoing
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("userId")]
        public long UserId { get; set; }
        [Column("outcome")]
        public long Outcome { get; set; }
        [Column("categoryId")]
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(OutgoingsCategory.Outgoings))]
        public virtual OutgoingsCategory Category { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Outgoings")]
        public virtual User User { get; set; }
    }
}
