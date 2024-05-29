using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimesControlByEmployeeIdPaginated;

internal class GetTimesControlByEmployeeIdPaginatedHandler(ITimeControlRepository timeControlRepository, IMapper mapper)
    : IQueryHandler<GetTimesControlByEmployeeIdPaginatedQuery, ResponseData<GetTimesControlByEmployeeIdPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetTimesControlByEmployeeIdPaginatedResponse>>> Handle(
        GetTimesControlByEmployeeIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var timeControls = timeControlRepository.GetWithUserByEmployeeIdQueryable(request.EmployeeId);

        // Si existe From y To filtra en rango, si solo existe From filtra en ese día.
        if (request.From != DateTimeOffset.MinValue && request.To != DateTimeOffset.MinValue)
        {
            timeControls = timeControls
                .Where(tc => tc.Start >= request.From && tc.Finish <= request.To);
        }
        else if (request.To == DateTimeOffset.MinValue && request.From != DateTimeOffset.MinValue)
        {
            timeControls = timeControls
                .Where(tc => tc.Start == request.From);
        }

        var resultResponse = await ResponseData<GetTimesControlByEmployeeIdPaginatedResponse>.CreateAsync(
            timeControls,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
