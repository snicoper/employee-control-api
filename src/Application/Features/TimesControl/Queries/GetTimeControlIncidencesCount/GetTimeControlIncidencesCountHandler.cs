using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlIncidencesCount;

internal class GetTimeControlIncidencesCountHandler(ITimeControlRepository timeControlRepository)
    : IQueryHandler<GetTimeControlIncidencesCountQuery, GetTimeControlIncidencesCountResponse>
{
    public Task<Result<GetTimeControlIncidencesCountResponse>> Handle(
        GetTimeControlIncidencesCountQuery request,
        CancellationToken cancellationToken)
    {
        var incidences = timeControlRepository.GetTimesControlIncidencesQueryable().Count();
        var resultResponse = new GetTimeControlIncidencesCountResponse(incidences);

        return Task.FromResult(Result.Success(resultResponse));
    }
}
