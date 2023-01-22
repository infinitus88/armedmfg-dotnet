using ArmedMFG.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .UseHiLo("department_hilo")
            .IsRequired();

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
