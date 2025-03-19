using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain.Models;

namespace ProductManagement.Infrastructure.Configurations;

public class ProductsConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProductName)
            .HasMaxLength(100);
        builder.Property(p => p.Description)
            .HasMaxLength(1000);
        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);
    }
}