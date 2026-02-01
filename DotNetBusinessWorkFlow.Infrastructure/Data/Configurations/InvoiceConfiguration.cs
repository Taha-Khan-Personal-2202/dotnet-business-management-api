using DotNetBusinessWorkFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBusinessWorkFlow.Infrastructure.Data.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.InvoiceNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.OrderId)
            .IsRequired();

        builder.Property(i => i.CustomerId)
            .IsRequired();

        builder.Property(i => i.IssuedAt)
            .IsRequired();

        builder.OwnsOne(i => i.TotalAmount, money =>
             {
                 money.Property(m => m.Amount)
                     .HasColumnName("TotalAmount")
                     .IsRequired();

                 money.Property(m => m.Currency)
                     .HasColumnName("Currency")
                     .HasMaxLength(3)
                     .IsRequired();
             });

        builder.HasIndex(i => i.InvoiceNumber)
            .IsUnique();

        builder.HasIndex(i => i.OrderId)
            .IsUnique();
    }
}
