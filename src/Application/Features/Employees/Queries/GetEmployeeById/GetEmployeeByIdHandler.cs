using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler(IIdentityService identityService, IMapper mapper)
    : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    public async Task<Result<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await identityService.GetByIdWithCompanyCalendarAsync(request.Id);
        var result = mapper.Map<User, GetEmployeeByIdResponse>(employee);

        return Result.Success(result);
    }
}
