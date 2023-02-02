using ArmedMFG.ApplicationCore.Entities.CustomerOrganizationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class CustomerOrganizationConfiguration : IEntityTypeConfiguration<CustomerOrganization>
{
    public void Configure(EntityTypeBuilder<CustomerOrganization> builder)
    {
        builder.Property(p => p.Id)
            .UseHiLo("customer_organization_hilo")
            .IsRequired(); 
        
        builder.OwnsOne(o => o.MainBranchAddress, a =>
        {
            a.WithOwner();

            a.Property(address => address.Street)
                .HasMaxLength(180)
                .IsRequired();

            a.Property(address => address.District)
                .HasMaxLength(90)
                .IsRequired();

            a.Property(address => address.Region)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Navigation(x => x.MainBranchAddress).IsRequired();
    }

}
