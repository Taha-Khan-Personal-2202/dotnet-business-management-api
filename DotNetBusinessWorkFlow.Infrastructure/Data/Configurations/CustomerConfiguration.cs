using DotNetBusinessWorkFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBusinessWorkFlow.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(c => c.Name)
               .IsRequired();

        builder.Property(c => c.Email)
               .IsRequired();

        builder.HasIndex(c => c.Email)
               .IsUnique();
    }
}
