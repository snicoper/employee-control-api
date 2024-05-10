using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.WorkingDaysWeek;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.WorkingDaysWeek.Queries.GetWorkingDaysWeek;

internal class GetWorkingDaysWeekHandler(IWorkingDaysWeekService workingDaysWeekService, IMapper mapper)
    : IQueryHandler<GetWorkingDaysWeekQuery, GetWorkingDaysWeekResponse>
{
    public async Task<Result<GetWorkingDaysWeekResponse>> Handle(
        GetWorkingDaysWeekQuery request,
        CancellationToken cancellationToken)
    {
        var workingDaysWeek = await workingDaysWeekService.GetWorkingDaysWeekAsync(cancellationToken);
        var resultResponse = mapper.Map<GetWorkingDaysWeekResponse>(workingDaysWeek);

        return Result.Success(resultResponse);
    }
}
