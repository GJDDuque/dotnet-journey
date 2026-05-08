using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

namespace OrdersApi.Data;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) 
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CustomerName = "Alice", Status = "New", Amount = 150m, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Order { Id = 2, CustomerName = "Bob", Status = "Shipped", Amount = 300m, CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Order { Id = 3, CustomerName = "Gonçalo", Status = "New", Amount = 75m, CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}