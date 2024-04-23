using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.EmployeeHolidays;

public class EmployeeHolidaysService(IApplicationDbContext context) : IEmployeeHolidaysService
{
    public async Task<EmployeeHoliday> GetByYearByEmployeeId(int year, string employeeId, CancellationToken cancellationToken)
    {
        var employeeHolidays = await context
                                   .EmployeeHolidays
                                   .SingleOrDefaultAsync(eh => eh.Year == year && eh.UserId == employeeId, cancellationToken) ??
                               throw new NotFoundException(nameof(EmployeeHoliday), nameof(EmployeeHoliday.Year));

        return employeeHolidays;
    }
}
