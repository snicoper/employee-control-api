using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;
using MediatR;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetEmployeeHolidaysByYearAndEmployeeId;

internal class GetEmployeeHolidaysByYearAndEmployeeIdHandler(IEmployeeHolidaysService employeeHolidaysService, IMapper mapper)
    : IRequestHandler<GetEmployeeHolidaysByYearAndEmployeeIdQuery, GetEmployeeHolidaysByYearAndEmployeeIdResponse>
{
    public async Task<GetEmployeeHolidaysByYearAndEmployeeIdResponse> Handle(
        GetEmployeeHolidaysByYearAndEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var employeeHolidays =
            await employeeHolidaysService.GetByYearByEmployeeId(request.Year, request.EmployeeId, cancellationToken);
        var resultResponse = mapper.Map<GetEmployeeHolidaysByYearAndEmployeeIdResponse>(employeeHolidays);

        return resultResponse;
    }
}
