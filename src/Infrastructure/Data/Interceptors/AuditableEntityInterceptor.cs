using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Domain.Common;
using EmployeeControl.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmployeeControl.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(ICurrentUserService currentUserService, IDateTime dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = currentUserService.Id;
                entry.Entity.Created = dateTime.UtcNow;
            }

            if (entry.State != EntityState.Added && entry.State != EntityState.Modified && !entry.HasChangedOwnedEntities())
            {
                continue;
            }

            entry.Entity.LastModifiedBy = currentUserService.Id;
            entry.Entity.LastModified = dateTime.UtcNow;
        }
    }
}
