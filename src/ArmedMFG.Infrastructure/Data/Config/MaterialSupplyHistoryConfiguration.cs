using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class MaterialSupplyHistoryConfiguration : IEntityTypeConfiguration<MaterialSupplyHistory>
{
    public void Configure(EntityTypeBuilder<MaterialSupplyHistory> builder)
    {
        builder.Property(msh => msh.Id)
            .UseHiLo("material_supply_history_hilo")
            .IsRequired();
        
        builder.Property(msh => msh.Amount)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");    
        
        builder.Property(msh => msh.UnitPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");
    }
}
