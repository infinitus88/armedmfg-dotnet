using ArmedMFG.ApplicationCore.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.ToTable("Products");

        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.HasOne(pt => pt.ProductCategory)
            .WithMany()
            .HasForeignKey(pt => pt.ProductCategoryId);
    }
}
