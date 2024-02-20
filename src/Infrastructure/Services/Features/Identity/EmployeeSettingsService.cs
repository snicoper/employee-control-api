using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Infrastructure.Services.Features.Identity;

public class EmployeeSettingsService(IApplicationDbContext context) : IEmployeeSettingsService
{
    public async Task<int> CreateAsync(EmployeeSettings employeeSettings, CancellationToken cancellationToken)
    {
        await context.EmployeeSettings.AddAsync(employeeSettings, cancellationToken);

        return await context.SaveChangesAsync(cancellationToken);
    }
}
