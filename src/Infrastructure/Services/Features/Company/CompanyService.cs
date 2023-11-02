using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using EmployeeControl.Application.Common.Interfaces.Features.Company;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Infrastructure.Data;

namespace EmployeeControl.Infrastructure.Services.Features.Company;

public class CompanyService(
        ApplicationDbContext context,
        ICompanyValidatorService companyValidatorService,
        ICompanySettingsService companySettingsService,
        IValidationFailureService validationFailureService)
    : ICompanyService
{
    public async Task<Domain.Entities.Company> CreateAsync(
        Domain.Entities.Company company,
        CancellationToken cancellationToken)
    {
        // Validaciones.
        await companyValidatorService.UniqueNameValidationAsync(company.Name, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        // Crear Company.
        await context.Companies.AddAsync(company, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Crear CompanySettings.
        var companySettings = new CompanySettings { CompanyId = company.Id };
        await companySettingsService.CreateAsync(companySettings, cancellationToken);

        return company;
    }
}
