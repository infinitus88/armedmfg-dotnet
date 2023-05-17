using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class MaterialSupplyConfiguration : IEntityTypeConfiguration<MaterialSupply>
{
    public void Configure(EntityTypeBuilder<MaterialSupply> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder.Property(ms => ms.Amount)
            .IsRequired(true);
        
        builder.Property(ms => ms.TotalPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");
    }
}
