using AccountHistory.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountHistory.DataAccess
{
    public class AccountHistoryContext : DbContext
    {
        public AccountHistoryContext(DbContextOptions<AccountHistoryContext> options)
        : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(o => o.Id);
            });

            modelBuilder.Entity<Operation>().HasData(
                new Operation { Id = 1, Name = "Credit" },
                new Operation { Id = 2, Name = "Debit" }
            );

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasOne(t => t.Operation)
                    .WithMany(o => o.Transactions)
                    .HasForeignKey(t => t.OperationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
