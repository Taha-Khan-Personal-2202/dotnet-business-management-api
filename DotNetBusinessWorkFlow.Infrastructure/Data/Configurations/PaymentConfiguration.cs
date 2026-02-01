using DotNetBusinessWorkFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBusinessWorkFlow.Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.Amount);

        builder.OwnsOne(p => p.Amount, price =>
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
