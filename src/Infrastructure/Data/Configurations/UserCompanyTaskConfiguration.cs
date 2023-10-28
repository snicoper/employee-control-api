using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class UserCompanyTaskConfiguration : IEntityTypeConfiguration<UserCompanyTask>
{
    public void Configure(EntityTypeBuilder<UserCompanyTask> builder)
    {
        // Key.
        builder.HasKey(uct => new { uct.UserId, uct.CompanyTaskId });

        // Indexes.
        builder.HasIndex(uct => new { uct.CompanyId, uct.UserId, uct.CompanyTaskId })
            .IsUnique();

        // Relations.
        builder.HasOne(uct => uct.User)
            .WithMany(au => au.UserCompanyTasks)
            .HasForeignKey(uct => uct.UserId);

        builder.HasOne(uct => uct.CompanyTask)
            .WithMany(ct => ct.UserCompanyTasks)
            .HasForeignKey(uct => uct.CompanyTaskId);
    }
}
