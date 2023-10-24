using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IIdentityService identityService,
        ICurrentUserService currentUserService)
    : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await userManager.FindByIdAsync(request.Id);

        await ValidateForReadInformationAsync(employee);

        var result = mapper.Map<ApplicationUser, GetEmployeeByIdResponse>(employee!);
        result.UserRoles = await userManager.GetRolesAsync(employee!);

        return result;
    }

    private async Task ValidateForReadInformationAsync(ApplicationUser? employee)
    {
        if (employee is null)
        {
            throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));
        }

        var currentUserId = currentUserService.Id;

        // Si al menos tiene un Role de Staff, devolver datos.
        if (currentUserId is not null && await identityService.IsInRoleAsync(currentUserId, Roles.Staff))
        {
            return;
        }

        // Comprobar si el que solicita el empleado pertenece a la misma empresa.
        if (currentUserService.CompanyId == employee.CompanyId)
        {
            return;
        }

        throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));
    }
}
