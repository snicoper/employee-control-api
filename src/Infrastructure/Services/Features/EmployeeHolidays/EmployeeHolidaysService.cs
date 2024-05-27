﻿using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.EmployeeHolidays;

public class EmployeeHolidaysService(IApplicationDbContext context) : IEmployeeHolidaysService
{
    public async Task<EmployeeHoliday> GetByEmployeeIdAndYearAsync(
        int year,
        string employeeId,
        CancellationToken cancellationToken)
    {
        var employeeHoliday = await context
                                  .EmployeeHolidays
                                  .SingleOrDefaultAsync(eh => eh.UserId == employeeId && eh.Year == year, cancellationToken)
                              ?? throw new NotFoundException(nameof(EmployeeHoliday), nameof(EmployeeHoliday.UserId));

        return employeeHoliday;
    }

    public async Task<bool> ExistsByYearAndEmployeeId(int year, string employeeId, CancellationToken cancellationToken)
    {
        var exists = await context
            .EmployeeHolidays
            .AnyAsync(eh => eh.Year == year && eh.UserId == employeeId, cancellationToken);

        return exists;
    }

    public async Task<EmployeeHoliday> CreateAsync(
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
