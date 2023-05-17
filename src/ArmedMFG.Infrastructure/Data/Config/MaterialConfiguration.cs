using ArmedMFG.ApplicationCore.Entities.MaterialAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(mt => mt.Id);

        builder.Property(mt => mt.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.HasOne(mt => mt.MaterialCategory)
            .WithMany()
            .HasForeignKey(mt => mt.MaterialCategoryId);
    }
}
