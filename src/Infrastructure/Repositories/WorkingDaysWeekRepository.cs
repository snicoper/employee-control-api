using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class WorkingDaysWeekRepository(IApplicationDbContext context)
    : IWorkingDaysWeekRepository
{
    public async Task<WorkingDaysWeek> GetWorkingDaysWeekAsync(CancellationToken cancellationToken)
    {
        var workDays = await context
                .WorkingDaysWeek
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(WorkingDaysWeek), nameof(WorkingDaysWeek.Id));

        return workDays;
    }

    public async Task<WorkingDaysWeek> UpdateAsync(
        WorkingDaysWeek workingDaysWeek,
        CancellationToken cancellationToken)
    {
        context.WorkingDaysWeek.Update(workingDaysWeek);
        await context.SaveChangesAsync(cancellationToken);

        return workingDaysWeek;
    }
}
