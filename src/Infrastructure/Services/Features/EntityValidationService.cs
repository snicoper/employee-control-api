using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Features;

public class EntityValidationService(
    ICurrentUserService currentUserService,
    IIdentityService identityService,
    ILogger<EntityValidationService> logger)
    : IEntityValidationService
{
    public async Task CheckEntityCompanyIsOwnerAsync<TEntity>(TEntity entity)
        where TEntity : ICompany
    {
        await CheckEntityCompanyIsOwnerAsync(entity, Roles.SiteStaff);
    }

    public async Task CheckEntityCompanyIsOwnerAsync<TEntity>(TEntity entity, string requiredRole)
        where TEntity : ICompany
    {
        var currentUserId = currentUserService.Id;

        // Si al menos tiene un Role requireRole.
        if (await identityService.IsInRoleAsync(currentUserId, requiredRole))
        {
            return;
        }

        // Comprobar si el que solicita la tarea pertenece a la misma compañía.
        if (currentUserService.CompanyId == entity.CompanyId)
        {
            return;
        }

        logger.LogWarning("Usuario {user}: Error al leer los datos {entity}", currentUserService.Id, entity.GetType().Name);
        throw new NotFoundException(entity.GetType().Name, "Id");
    }
}
