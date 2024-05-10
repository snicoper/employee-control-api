using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;

internal class GetTimeStateByEmployeeIdHandler(ITimesControlService timesControlService, IIdentityService identityService)
    : IQueryHandler<GetTimeStateByEmployeeIdQuery, GetTimeStateByEmployeeIdResponse>
{
    public async Task<Result<GetTimeStateByEmployeeIdResponse>> Handle(
        GetTimeStateByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await identityService.GetByIdAsync(request.EmployeeId);
        var timeState = await timesControlService.GetTimeStateByEmployeeAsync(employee, cancellationToken);
        var resultResponse = new GetTimeStateByEmployeeIdResponse(timeState);

        return Result.Success(resultResponse);
    }
}
