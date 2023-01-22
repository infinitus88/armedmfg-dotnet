using ArmedMFG.BlazorShared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Id)
            .UseHiLo("product_category_hilo")
            .IsRequired();

        builder.Property(pc => pc.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
