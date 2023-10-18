using EmployeeControl.Domain.Entities;
using EmployeeControl.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class CompanyConfiguration : BaseAuditableEntityConfiguration<Company>, IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Primary key.
        builder.HasKey(c => c.Id);

        // Indexes.
        builder.HasIndex(c => c.Id);

        builder.HasIndex(c => c.Name)
            .IsUnique();

        // Relations.
        builder.HasMany(c => c.ApplicationUsers)
            .WithOne(au => au.Company)
            .OnDelete(DeleteBehavior.Cascade);

        // Properties.
        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        // Auditable entities.
        ConfigureAuditableEntities(builder);
    }
}
