using ArmedMFG.ApplicationCore.Entities.MaterialTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class MaterialTypeConfiguration : IEntityTypeConfiguration<MaterialType>
{
    public void Configure(EntityTypeBuilder<MaterialType> builder)
    {
        builder.Property(mt => mt.Id)
            .UseHiLo("material_type_hilo")
            .IsRequired();

        builder.Property(mt => mt.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.HasOne(mt => mt.MaterialCategory)
            .WithMany()
            .HasForeignKey(mt => mt.MaterialCategoryId);
    }
}
