using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetOrCreateEmployeeHolidaysByYearAndEmployeeId;

internal class GetOrCreateEmployeeHolidaysByYearAndEmployeeIdHandler(
    IEmployeeHolidaysRepository employeeHolidaysRepository,
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

        if (!await employeeHolidaysRepository.ExistsByYearAndEmployeeId(request.Year, request.EmployeeId, cancellationToken))
        {
            employeeHoliday = await employeeHolidaysRepository.CreateAsync(request.Year, request.EmployeeId, cancellationToken);
            created = true;
        }
        else
        {
            employeeHoliday = await employeeHolidaysRepository
                .GetByEmployeeIdAndYearAsync(request.Year, request.EmployeeId, cancellationToken);
        }

        return (employeeHoliday, created);
    }
}
