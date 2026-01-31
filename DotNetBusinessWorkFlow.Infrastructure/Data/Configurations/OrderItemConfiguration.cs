using DotNetBusinessWorkflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBusinessWorkFlow.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Quantity)
               .IsRequired();

        builder.OwnsOne(oi => oi.UnitPrice);

        builder.OwnsOne(p => p.UnitPrice, price =>
        {
            price.Property(m => m.Amount)
                 .HasColumnName("PriceAmount")
                 .IsRequired();

            price.Property(m => m.Currency)
                 .HasColumnName("PriceCurrency")
                 .IsRequired();
        });
    }
}
