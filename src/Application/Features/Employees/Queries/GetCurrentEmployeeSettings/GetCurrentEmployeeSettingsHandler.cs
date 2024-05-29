using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployeeSettings;

internal class GetCurrentEmployeeSettingsHandler(
    IEmployeeSettingsRepository employeeSettingsRepository,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IQueryHandler<GetCurrentEmployeeSettingsQuery, GetCurrentEmployeeSettingsResponse>
{
    public async Task<Result<GetCurrentEmployeeSettingsResponse>> Handle(
        GetCurrentEmployeeSettingsQuery request,
        CancellationToken cancellationToken)
    {
        var employeeSettings = await employeeSettingsRepository.GetByEmployeeIdAsync(currentUserService.Id, cancellationToken);
        var resultResponse = mapper.Map<GetCurrentEmployeeSettingsResponse>(employeeSettings);

        return Result.Success(resultResponse);
    }
}
