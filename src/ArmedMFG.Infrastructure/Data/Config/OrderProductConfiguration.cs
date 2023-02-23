using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.Property(op => op.Id)
            .UseHiLo("order_product_hilo")
            .IsRequired();
        
        builder.Property(op => op.SingleTimePrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(op => op.HaveSingleTimePrice)
            .IsRequired();
    }
}
