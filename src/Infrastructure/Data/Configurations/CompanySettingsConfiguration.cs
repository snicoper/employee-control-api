using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

internal class CompanySettingsConfiguration : IEntityTypeConfiguration<CompanySettings>
{
    public void Configure(EntityTypeBuilder<CompanySettings> builder)
    {
        // Table name.
        builder.ToTable("CompanySettings");

        // Key.
        builder.HasKey(cs => cs.Id);

        // Properties.
        builder.Property(cs => cs.Timezone)
            .HasMaxLength(50);

        builder.Property(cs => cs.PeriodTimeControlMax)
            .IsRequired();

        builder.Property(cs => cs.WeeklyWorkingHours)
            .IsRequired();

        builder.Property(cs => cs.GeolocationRequired);
    }
}
