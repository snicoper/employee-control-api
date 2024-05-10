using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployee;

internal class GetCurrentEmployeeHandler(IIdentityService identityService, IMapper mapper)
    : IQueryHandler<GetCurrentEmployeeQuery, GetCurrentEmployeeResponse>
{
    public async Task<Result<GetCurrentEmployeeResponse>> Handle(
        GetCurrentEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var user = await identityService.GetCurrentAsync();
        var resultResponse = mapper.Map<GetCurrentEmployeeResponse>(user);

        return Result.Success(resultResponse);
    }
}
