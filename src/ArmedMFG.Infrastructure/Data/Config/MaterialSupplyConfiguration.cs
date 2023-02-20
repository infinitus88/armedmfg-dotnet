using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class MaterialSupplyConfiguration : IEntityTypeConfiguration<MaterialSupply>
{
    public void Configure(EntityTypeBuilder<MaterialSupply> builder)
    {
        builder.Property(ms => ms.Id)
            .UseHiLo("material_supply_hilo")
            .IsRequired();
        
        builder.Property(ms => ms.Amount)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");    
        
        builder.Property(ms => ms.Price)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");
    }
}
