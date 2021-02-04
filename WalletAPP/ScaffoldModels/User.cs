using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WalletDB.ScaffoldModels
{
    [Index(nameof(Nickname), IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Outgoings = new HashSet<Outgoing>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("nickname", TypeName = "VARCHAR")]
        public string Nickname { get; set; }
        [Column("money")]
        public long Money { get; set; }

        [InverseProperty("User")]
        public virtual Incom Incom { get; set; }
        [InverseProperty(nameof(Outgoing.User))]
        public virtual ICollection<Outgoing> Outgoings { get; set; }
    }
}
