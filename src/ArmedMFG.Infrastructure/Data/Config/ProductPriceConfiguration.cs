using ArmedMFG.ApplicationCore.Entities.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        
        // builder.ToTable("");

        builder.Property(p => p.Id)
            .UseHiLo("product_price_hilo")
            .IsRequired();
        
        builder.Property(p => p.Price)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        // builder.HasOne(p => p.ProductType)
        //     .WithMany()
        //     .HasForeignKey(p => p.ProductTypeId);
    }
}
