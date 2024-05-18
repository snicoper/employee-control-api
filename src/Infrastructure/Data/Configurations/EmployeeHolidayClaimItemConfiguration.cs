using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

internal class EmployeeHolidayClaimItemConfiguration : IEntityTypeConfiguration<EmployeeHolidayClaimItem>
{
    public void Configure(EntityTypeBuilder<EmployeeHolidayClaimItem> builder)
    {
        // Table name.
        builder.ToTable("EmployeeHolidayClaimItems");

        // Key.
        builder.HasKey(ehcl => ehcl.Id);

        // Indexes.
        builder.HasIndex(ehcl => ehcl.Id);

        builder.HasIndex(ehcl => new { ehcl.Date, ehcl.UserId })
            .IsUnique();

        // One-to-Many.
        builder.HasOne<User>(ehcl => ehcl.User)
            .WithMany(au => au.EmployeeHolidayClaimItems)
            .HasForeignKey(ehcl => ehcl.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne<EmployeeHolidayClaim>(ehcl => ehcl.EmployeeHolidayClaim)
            .WithMany(au => au.EmployeeHolidayClaimLines)
            .HasForeignKey(ehcl => ehcl.EmployeeHolidayClaimId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Properties.
        builder.Property(ehcl => ehcl.Date)
            .IsRequired();

        builder.Property(ehcl => ehcl.UserId)
            .IsRequired();

        builder.Property(ehcl => ehcl.EmployeeHolidayClaimId)
            .IsRequired();
    }
}
