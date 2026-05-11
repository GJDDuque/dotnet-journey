using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

namespace OrdersApi.Data;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) 
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "Alice", Email = "alice@example.com", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Customer { Id = 2, Name = "Bob", Email = "bob@example.com", CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Customer { Id = 3, Name = "Gonçalo", Email = "goncalo@example.com", CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, Status = "New", Amount = 150m, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), CustomerId = 1 },
            new Order { Id = 2, Status = "Shipped", Amount = 300m, CreatedAt = new DateTime(2025, 1, 2, 0, 0, 0, DateTimeKind.Utc), CustomerId = 2 },
            new Order { Id = 3, Status = "New", Amount = 75m, CreatedAt = new DateTime(2025, 1, 3, 0, 0, 0, DateTimeKind.Utc), CustomerId = 3 }
        );
    }
}