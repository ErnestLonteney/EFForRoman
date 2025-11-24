using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Code);
        builder.Property(p => p.Name)
            .HasMaxLength(200);
        builder.Property(p => p.Price)
            .HasColumnType("money");
        builder.HasIndex(c => c.Name).IsUnique(false);
        builder.Property<decimal>("SellPrice").HasComputedColumnSql("(Price * 1.2)");
    }
}