using ArmedMFG.BlazorShared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class MaterialCategoryConfiguration : IEntityTypeConfiguration<MaterialCategory>
{
    public void Configure(EntityTypeBuilder<MaterialCategory> builder)
    {
        builder.HasKey(mc => mc.Id);

        builder.Property(mc => mc.Id)
            .UseHiLo("material_category_hilo")
            .IsRequired();

        builder.Property(mc => mc.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
