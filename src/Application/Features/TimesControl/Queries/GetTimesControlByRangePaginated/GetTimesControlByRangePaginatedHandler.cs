using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimesControlByRangePaginated;

public class GetTimesControlByRangePaginatedHandler(ITimeControlRepository timeControlRepository, IMapper mapper)
    : IQueryHandler<GetTimesControlByRangePaginatedQuery, ResponseData<GetTimesControlByRangePaginatedResponse>>
{
    public async Task<Result<ResponseData<GetTimesControlByRangePaginatedResponse>>> Handle(
        GetTimesControlByRangePaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var timeControls = timeControlRepository.GetWithUserQueryable();

        // Si existe From y To filtra en rango, si solo existe From filtra en ese día.
        if (request.From != DateTimeOffset.MinValue && request.To != DateTimeOffset.MinValue)
        {
            timeControls = timeControls
                .Where(
                    tc => (tc.Start >= request.From && tc.Start <= request.To) ||
                          (tc.Finish <= request.To && tc.Finish >= request.From));
        }
        else if (request.To == DateTimeOffset.MinValue && request.From != DateTimeOffset.MinValue)
        {
            timeControls = timeControls
                .Where(tc => tc.Start == request.From || tc.Finish == request.From);
        }

        var resultResponse = await ResponseData<GetTimesControlByRangePaginatedResponse>.CreateAsync(
            timeControls,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
