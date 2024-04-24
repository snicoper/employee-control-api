using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class CompanyCalendarHolidayConfiguration : IEntityTypeConfiguration<CompanyCalendarHoliday>
{
    public void Configure(EntityTypeBuilder<CompanyCalendarHoliday> builder)
    {
        builder.ToTable("CompanyCalendarHolidays");

        // Key.
        builder.HasKey(ch => ch.Id);

        // Indexes.
        builder.HasIndex(ch => ch.Id);

        builder.HasIndex(ch => new { Day = ch.Date })
            .IsUnique();

        // Properties.
        builder.Property(ch => ch.Date)
            .IsRequired();

        builder.Property(ch => ch.Description)
            .IsRequired()
            .HasMaxLength(50);
    }
}
