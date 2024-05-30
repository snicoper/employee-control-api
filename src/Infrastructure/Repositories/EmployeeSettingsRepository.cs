using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class EmployeeSettingsRepository(IApplicationDbContext context)
    : IEmployeeSettingsRepository
{
    public async Task<EmployeeSettings> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await context
                .EmployeeSettings
                .SingleOrDefaultAsync(es => es.UserId == employeeId, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeSettings), nameof(EmployeeSettings.UserId));

        return employee;
    }

    public async Task<int> CreateAsync(EmployeeSettings employeeSettings, CancellationToken cancellationToken)
    {
        await context.EmployeeSettings.AddAsync(employeeSettings, cancellationToken);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmployeeSettings> UpdateAsync(EmployeeSettings employeeSettings, CancellationToken cancellationToken)
    {
        context.EmployeeSettings.Update(employeeSettings);

        await context.SaveChangesAsync(cancellationToken);

        return employeeSettings;
    }
}
