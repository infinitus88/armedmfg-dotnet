using ArmedMFG.ApplicationCore.Entities.ProductionReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class ProductionReportConfiguration : IEntityTypeConfiguration<ProductionReport>
{
    public void Configure(EntityTypeBuilder<ProductionReport> builder)
    {
        builder.HasKey(pb => pb.Id);
    }
}
