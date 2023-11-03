using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;

internal class GetTimeStateByEmployeeIdHandler(ITimesControlService timesControlService)
    : IRequestHandler<GetTimeStateByEmployeeIdQuery, GetTimeStateByEmployeeIdResponse>
{
    public async Task<GetTimeStateByEmployeeIdResponse> Handle(
        GetTimeStateByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeState = await timesControlService.GetTimeStateByEmployeeIAsync(request.EmployeeId, cancellationToken);
        var resultResponse = new GetTimeStateByEmployeeIdResponse(timeState);

        return resultResponse;
    }
}
