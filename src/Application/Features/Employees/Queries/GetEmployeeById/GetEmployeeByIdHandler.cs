using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler(
    IIdentityService identityService,
    IMapper mapper,
    IEntityValidationService entityValidationService)
    : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await identityService.GetByIdAsync(request.Id);
        await entityValidationService.CheckEntityCompanyIsOwnerAsync(employee);

        var result = mapper.Map<ApplicationUser, GetEmployeeByIdResponse>(employee!);

        return result;
    }
}
