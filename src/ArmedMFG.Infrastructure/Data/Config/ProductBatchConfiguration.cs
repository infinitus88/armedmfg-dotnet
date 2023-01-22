using ArmedMFG.ApplicationCore.Entities.ProductBatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class ProductBatchConfiguration : IEntityTypeConfiguration<ProductBatch>
{
    public void Configure(EntityTypeBuilder<ProductBatch> builder)
    {
        builder.HasKey(pb => pb.Id);

        builder.Property(pb => pb.Id)
            .UseHiLo("product_batch_hilo")
            .IsRequired();
    }
}
