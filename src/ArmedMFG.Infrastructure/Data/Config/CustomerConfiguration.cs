using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        
        // builder.ToTable("");

        builder.Property(p => p.Id)
            .UseHiLo("customer_hilo")
            .IsRequired();

        // builder.HasOne(p => p.ProductType)
        //     .WithMany()
        //     .HasForeignKey(p => p.ProductTypeId);
    }
}
