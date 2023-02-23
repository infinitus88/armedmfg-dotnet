using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
                
        // builder.ToTable("");

        builder.Property(pt => pt.Id)
            .UseHiLo("product_type_hilo")
            .IsRequired();

        builder.Property(pt => pt.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(pt => pt.PictureUri)
            .IsRequired(false);

        builder.HasOne(pt => pt.ProductCategory)
            .WithMany()
            .HasForeignKey(pt => pt.ProductCategoryId);
    }
}
