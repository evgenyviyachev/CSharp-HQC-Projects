namespace BankSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations.Schema;
    using Models;

    public class BankSystemContext : DbContext
    {
        public BankSystemContext()
            : base("name=BankSystemContext")
        {
        }

        public virtual IDbSet<Account> Accounts { get; set; }
        public virtual IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountId);

            modelBuilder.Entity<Account>()
                .Property(a => a.AccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasPrecision(8, 2)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Accounts)
                .WithRequired(a => a.User)
                .Map(a =>
                {
                    a.MapKey("UserId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .Map<SavingsAccount>(sa =>
                {
                    sa.Requires("AccountType")
                    .HasValue("Savings Account");
                })
                .Map<CheckingAccount>(ca =>
                {
                    ca.Requires("AccountType")
                    .HasValue("Checking Account");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
