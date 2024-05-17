using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;

internal class GetTimeStateByEmployeeIdHandler(ITimesControlService timesControlService, IUserRepository userRepository)
    : IQueryHandler<GetTimeStateByEmployeeIdQuery, GetTimeStateByEmployeeIdResponse>
{
    public async Task<Result<GetTimeStateByEmployeeIdResponse>> Handle(
        GetTimeStateByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdAsync(request.EmployeeId);
        var timeState = await timesControlService.GetTimeStateByEmployeeAsync(employee, cancellationToken);
        var resultResponse = new GetTimeStateByEmployeeIdResponse(timeState);

        return Result.Success(resultResponse);
    }
}
