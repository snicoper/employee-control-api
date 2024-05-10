using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployeeSettings;

internal class GetCurrentEmployeeSettingsHandler(
    IEmployeeSettingsService employeeSettingsService,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IQueryHandler<GetCurrentEmployeeSettingsQuery, GetCurrentEmployeeSettingsResponse>
{
    public async Task<Result<GetCurrentEmployeeSettingsResponse>> Handle(
        GetCurrentEmployeeSettingsQuery request,
        CancellationToken cancellationToken)
    {
        var employeeSettings = await employeeSettingsService.GetByEmployeeIdAsync(currentUserService.Id, cancellationToken);
        var resultResponse = mapper.Map<GetCurrentEmployeeSettingsResponse>(employeeSettings);

        return Result.Success(resultResponse);
    }
}
