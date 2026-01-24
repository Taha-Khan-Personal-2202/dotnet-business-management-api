using DotNetBusinessWorkflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBusinessWorkFlow.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Status)
               .IsRequired();

        builder.OwnsOne(o => o.TotalAmount);

        builder
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
