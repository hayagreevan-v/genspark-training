using System.Data.Common;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Contexts
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.AccountNo);

            modelBuilder.Entity<Transaction>().HasOne(t => t.FromUser)
                                                .WithMany(u => u.SentTransactions)
                                                .HasForeignKey(t => t.FromAccountNo)
                                                .HasConstraintName("FK_transaction_user_from")
                                                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>().HasOne(t => t.ToUser)
                                                .WithMany(u => u.RecievedTransactions)
                                                .HasForeignKey(t => t.ToAccountNo)
                                                .HasConstraintName("FK_transaction_user_to")
                                                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}