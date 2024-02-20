using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class EmployeeSettingsConfiguration : IEntityTypeConfiguration<EmployeeSettings>
{
    public void Configure(EntityTypeBuilder<EmployeeSettings> builder)
    {
        builder.ToTable("EmployeeSettings");

        // Key.
        builder.HasIndex(us => us.Id);

        // Indexes
        builder.HasIndex(cs => cs.UserId)
            .IsUnique();

        // Properties.
        builder.Property(cs => cs.UserId)
            .IsRequired();
    }
}
