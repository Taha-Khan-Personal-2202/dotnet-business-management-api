using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace DotNetBusinessWorkFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // AUTH
    public DbSet<User> Users => Set<User>();

    // BUSINESS
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        // Seed data
        UserSeed.Seed(modelBuilder);
    }
}
