using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetOrCreateEmployeeHolidaysByYearAndEmployeeId;

internal class GetOrCreateEmployeeHolidaysByYearAndEmployeeIdHandler(
    IEmployeeHolidaysService employeeHolidaysService,
    IMapper mapper)
    : IQueryHandler<GetOrCreateEmployeeHolidaysByYearAndEmployeeIdQuery, GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse>
{
    public async Task<Result<GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse>> Handle(
        GetOrCreateEmployeeHolidaysByYearAndEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var (created, employeeHoliday) = await employeeHolidaysService.GetOrCreateByYearByEmployeeIdAsync(
            request.Year,
            request.EmployeeId,
            cancellationToken);

        var resultResponse = mapper.Map<GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse>(employeeHoliday);
        resultResponse.Created = created;

        return Result.Success(resultResponse);
    }
}
