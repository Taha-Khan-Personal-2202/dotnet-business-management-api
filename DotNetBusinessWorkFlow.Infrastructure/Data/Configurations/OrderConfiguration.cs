using DotNetBusinessWorkFlow.Domain.Entities;
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

        builder.OwnsOne(p => p.TotalAmount, price =>
        {
            price.Property(m => m.Amount)
                 .HasColumnName("PriceAmount")
                 .IsRequired();

            price.Property(m => m.Currency)
                 .HasColumnName("PriceCurrency")
                 .IsRequired();
        });

        builder
            .HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
