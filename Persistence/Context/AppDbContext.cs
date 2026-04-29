using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This tells SQL to use 18 digits total, 2 after the decimal point
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Register)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RegisterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>(entity =>
            {
                // For Money: $99,999,999.99
                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

                // For Stock: 10.555 kg (3 decimals for weight/partial items)
                entity.Property(p => p.Stock)
                      .HasPrecision(18, 3);
            });

        }
    }
}
