using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

internal class CompanyCalendarConfiguration : IEntityTypeConfiguration<CompanyCalendar>
{
    public void Configure(EntityTypeBuilder<CompanyCalendar> builder)
    {
        // Table name.
        // Table name.
        builder.ToTable("CompanyCalendars");

        // Key.
        builder.HasKey(cc => cc.Id);

        // Indexes.
        builder.HasIndex(cc => cc.Name)
            .IsUnique();

        // Properties.
        builder.Property(cc => cc.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(cc => cc.Description)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(cc => cc.Default);
    }
}
