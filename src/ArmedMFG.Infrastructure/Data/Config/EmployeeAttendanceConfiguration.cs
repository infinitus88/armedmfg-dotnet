using ArmedMFG.ApplicationCore.Entities.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArmedMFG.Infrastructure.Data.Config;

public class EmployeeAttendanceConfiguration : IEntityTypeConfiguration<EmployeeAttendance>
{
    public void Configure(EntityTypeBuilder<EmployeeAttendance> builder)
    {
        builder
            .HasOne(ea => ea.Calendar)
            .WithMany()
            .HasForeignKey(ea => ea.Date)
            .HasPrincipalKey(c => c.Date);
    }
}
