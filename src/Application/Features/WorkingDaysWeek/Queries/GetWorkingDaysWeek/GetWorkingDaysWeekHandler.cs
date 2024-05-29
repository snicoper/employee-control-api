using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.WorkingDaysWeek.Queries.GetWorkingDaysWeek;

internal class GetWorkingDaysWeekHandler(IWorkingDaysWeekRepository workingDaysWeekRepository, IMapper mapper)
    : IQueryHandler<GetWorkingDaysWeekQuery, GetWorkingDaysWeekResponse>
{
    public async Task<Result<GetWorkingDaysWeekResponse>> Handle(
        GetWorkingDaysWeekQuery request,
        CancellationToken cancellationToken)
    {
        var workingDaysWeek = await workingDaysWeekRepository.GetWorkingDaysWeekAsync(cancellationToken);
        var resultResponse = mapper.Map<GetWorkingDaysWeekResponse>(workingDaysWeek);

        return Result.Success(resultResponse);
    }
}
