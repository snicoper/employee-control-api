using EmployeeControl.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeControl.Infrastructure.Data.Common;

public abstract class BaseAuditableEntityConfiguration<TEntity>
    where TEntity : BaseAuditableEntity
{
    protected void ConfigureAuditableEntities(EntityTypeBuilder<TEntity> builder)
    {
        // Properties.
        builder.Property(c => c.Created);
        builder.Property(c => c.CreatedBy);
        builder.Property(c => c.LastModified);
        builder.Property(c => c.LastModifiedBy);
    }
}
