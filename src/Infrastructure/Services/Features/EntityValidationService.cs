using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Infrastructure.Services.Features;

public class EntityValidationService(ICurrentUserService currentUserService, IIdentityService identityService)
    : IEntityValidationService
{
    public async Task CheckEntityCompanyIsOwner<TEntity>(TEntity entity)
        where TEntity : ICompany
    {
        var currentUserId = currentUserService.Id;

        // Si al menos tiene un Role de Staff Ok.
        if (await identityService.IsInRoleAsync(currentUserId, Roles.Staff))
        {
            return;
        }

        // Comprobar si el que solicita la tarea pertenece a la misma compañía.
        if (currentUserService.CompanyId == entity.CompanyId)
        {
            return;
        }

        throw new NotFoundException(nameof(entity), "Id");
    }
}
