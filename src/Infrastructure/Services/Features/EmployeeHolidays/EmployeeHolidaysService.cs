using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.EmployeeHolidays;

public class EmployeeHolidaysService(IApplicationDbContext context) : IEmployeeHolidaysService
{
    public async Task<(bool Created, EmployeeHoliday EmployeeHoliday)> GetOrCreateByYearByEmployeeIdAsync(
        int year,
        string employeeId,
        CancellationToken cancellationToken)
    {
        var employeeHoliday = await context
            .EmployeeHolidays
            .SingleOrDefaultAsync(eh => eh.Year == year && eh.UserId == employeeId, cancellationToken);

        if (employeeHoliday is not null)
        {
            return (false, employeeHoliday);
        }

        employeeHoliday = await CreateEmployeeHolidayAsync(year, employeeId, cancellationToken);

        return (true, employeeHoliday);
    }

    private async Task<EmployeeHoliday> CreateEmployeeHolidayAsync(
        int year,
        string employeeId,
        CancellationToken cancellationToken)
    {
        var employeeHoliday = new EmployeeHoliday { Year = year, TotalDays = 0, ConsumedDays = 0, UserId = employeeId };

        context.EmployeeHolidays.Add(employeeHoliday);
        await context.SaveChangesAsync(cancellationToken);

        return employeeHoliday;
    }
}
