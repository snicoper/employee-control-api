using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlIncidencesCount;

internal class GetTimeControlIncidencesCountHandler(ITimesControlService timesControlService)
    : IQueryHandler<GetTimeControlIncidencesCountQuery, GetTimeControlIncidencesCountResponse>
{
    public Task<Result<GetTimeControlIncidencesCountResponse>> Handle(
        GetTimeControlIncidencesCountQuery request,
        CancellationToken cancellationToken)
    {
        var incidences = timesControlService.GetTimesControlIncidencesQueryable().Count();
        var resultResponse = new GetTimeControlIncidencesCountResponse(incidences);

        return Task.FromResult(Result.Success(resultResponse));
    }
}
