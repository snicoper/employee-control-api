using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Indexes.
        builder.HasIndex(au => au.Email)
            .IsUnique();

        builder.HasIndex(au => au.RefreshToken)
            .IsUnique();

        // Relations.
        builder.HasOne(au => au.Company)
            .WithMany(c => c.ApplicationUsers)
            .IsRequired();

        // Properties.
        builder.Property(au => au.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(au => au.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(au => au.RefreshToken)
            .HasMaxLength(50);

        builder.Property(au => au.RefreshTokenExpiryTime);
    }
}
