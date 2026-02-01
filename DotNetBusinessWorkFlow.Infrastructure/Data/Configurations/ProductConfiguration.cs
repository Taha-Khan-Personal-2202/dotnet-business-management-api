using DotNetBusinessWorkFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBusinessWorkFlow.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.Price);
        builder.OwnsOne(p => p.Price, price =>
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

