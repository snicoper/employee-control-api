using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class EmployeeHolidayConfiguration : IEntityTypeConfiguration<EmployeeHoliday>
{
    public void Configure(EntityTypeBuilder<EmployeeHoliday> builder)
    {
        builder.ToTable("EmployeeHolidays");

        // Key.
        builder.HasKey(d => d.Id);

        // Indexes.
        builder.HasIndex(d => d.Id);

        builder.HasIndex(d => new { d.Year, d.UserId })
            .IsUnique();

        // Properties.
        builder.Property(d => d.Year)
            .IsRequired();

        builder.Property(d => d.TotalDays)
            .IsRequired();

        builder.Property(d => d.Consumed)
            .IsRequired();

        builder.Property(d => d.UserId)
            .IsRequired();
    }
}
