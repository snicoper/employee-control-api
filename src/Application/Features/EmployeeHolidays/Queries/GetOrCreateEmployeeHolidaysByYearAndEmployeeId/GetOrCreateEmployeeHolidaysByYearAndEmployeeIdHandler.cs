using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

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
        var (employeeHoliday, created) = await GetOrCreateEmployeeHolidaysByYearAndEmployeeId(
            request,
            cancellationToken);

        var resultResponse = mapper.Map<GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse>(employeeHoliday);
        resultResponse.Created = created;

        return Result.Success(resultResponse);
    }

    private async Task<(EmployeeHoliday EmployeeHoliday, bool Created)> GetOrCreateEmployeeHolidaysByYearAndEmployeeId(
        GetOrCreateEmployeeHolidaysByYearAndEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        EmployeeHoliday employeeHoliday;
        var created = false;

        if (!await employeeHolidaysService.ExistsByYearAndEmployeeId(request.Year, request.EmployeeId, cancellationToken))
        {
            employeeHoliday = await employeeHolidaysService.CreateAsync(request.Year, request.EmployeeId, cancellationToken);
            created = true;
        }
        else
        {
            employeeHoliday = await employeeHolidaysService
                .GetByEmployeeIdAndYearAsync(request.Year, request.EmployeeId, cancellationToken);
        }

        return (employeeHoliday, created);
    }
}
