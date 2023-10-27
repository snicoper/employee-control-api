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
            .HasForeignKey(au => au.CompanyId);

        // Properties.
        builder.Property(au => au.CompanyId)
            .IsRequired();

        builder.Property(au => au.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(au => au.FirstName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(au => au.LastName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(au => au.RefreshToken)
            .HasMaxLength(256);

        builder.Property(au => au.RefreshTokenExpiryTime);
        builder.Property(au => au.EntryDate);
    }
}
