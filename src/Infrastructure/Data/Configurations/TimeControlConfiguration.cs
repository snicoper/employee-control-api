﻿using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Configurations;

public class TimeControlConfiguration : IEntityTypeConfiguration<TimeControl>
{
    public void Configure(EntityTypeBuilder<TimeControl> builder)
    {
        // Key.
        builder.HasKey(tc => tc.Id);

        // Indexes.
        builder.HasIndex(tc => tc.Id);

        // Relations.
        builder.HasOne(tc => tc.User)
            .WithMany(au => au.TimeControls)
            .HasForeignKey(tc => tc.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Properties
        builder.Property(tc => tc.Start)
            .IsRequired();

        builder.Property(tc => tc.Finish)
            .IsRequired();

        builder.Property(tc => tc.ClosedBy)
            .IsRequired();

        builder.Property(tc => tc.TimeState)
            .IsRequired();

        builder.Property(tc => tc.CompanyId)
            .IsRequired();

        builder.Property(tc => tc.UserId)
            .IsRequired();
    }
}
