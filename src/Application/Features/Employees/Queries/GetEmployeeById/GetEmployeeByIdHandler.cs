using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Entities;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IEntityValidationService entityValidationService)
    : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await userManager.FindByIdAsync(request.Id) ??
                       throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        await entityValidationService.CheckEntityCompanyIsOwner(employee);

        var result = mapper.Map<ApplicationUser, GetEmployeeByIdResponse>(employee!);

        return result;
    }
}
