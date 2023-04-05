using ArmedMFG.ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.Id)
            .UseHiLo("order_product_hilo")
            .IsRequired();

        builder.Property(o => o.TotalAmount)
            .IsRequired(true)
            .HasColumnType("decimal(18, 2)");
    }
}
