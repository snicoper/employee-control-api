using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
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
