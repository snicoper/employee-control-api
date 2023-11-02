using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Infrastructure.Services.Features.CompaniesSettings;

public class CompanySettingsService(IApplicationDbContext context) : ICompanySettingsService
{
    public async Task<CompanySettings> CreateAsync(CompanySettings companySettings, CancellationToken cancellationToken)
    {
        await context.CompanySettings.AddAsync(companySettings, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return companySettings;
    }
}
