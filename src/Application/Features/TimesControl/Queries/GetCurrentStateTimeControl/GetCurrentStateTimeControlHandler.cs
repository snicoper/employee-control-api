using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetCurrentStateTimeControl;

internal class GetCurrentStateTimeControlHandler(ITimesControlService timesControlService)
    : IRequestHandler<GetCurrentStateTimeControlQuery, GetCurrentStateTimeControlResponse>
{
    public async Task<GetCurrentStateTimeControlResponse> Handle(
        GetCurrentStateTimeControlQuery request,
        CancellationToken cancellationToken)
    {
        var unclosed = await timesControlService.GetCurrentStateByEmployeeIdAsync(request.EmployeeId, cancellationToken);
        var resultResponse = new GetCurrentStateTimeControlResponse(unclosed);

        return resultResponse;
    }
}
