using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NorthwindEFCore.ScaffoldModels
{
    public partial class Wallet : DbContext
    {
        public Wallet()
        {
        }

        public Wallet(DbContextOptions<Wallet> options)
            : base(options)
        {
        }

        public virtual DbSet<Incom> Incoms { get; set; }
        public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }
        public virtual DbSet<Outgoing> Outgoings { get; set; }
        public virtual DbSet<OutgoingsCategory> OutgoingsCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Filename=walletDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incom>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Incoms)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Incom)
                    .HasForeignKey<Incom>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Outgoing>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Outgoings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Outgoings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
