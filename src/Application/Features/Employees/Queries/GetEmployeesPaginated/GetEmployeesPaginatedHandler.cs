using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;

internal class GetEmployeesPaginatedHandler(IIdentityService identityService, IMapper mapper)
    : IQueryHandler<GetEmployeesPaginatedQuery, ResponseData<GetEmployeesPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetEmployeesPaginatedResponse>>> Handle(
        GetEmployeesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = identityService.GetAllQueryable();

        var resultResponse = await ResponseData<GetEmployeesPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
