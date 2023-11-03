using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Company;
using EmployeeControl.Infrastructure.Data;

namespace EmployeeControl.Infrastructure.Services.Features.Company;

public class CompanyService(
        ApplicationDbContext context,
        ICompanyValidatorService companyValidatorService,
        IValidationFailureService validationFailureService)
    : ICompanyService
{
    public async Task<Domain.Entities.Company> CreateAsync(
        Domain.Entities.Company company,
        string timezone,
        CancellationToken cancellationToken)
    {
        // Validaciones.
        await companyValidatorService.UniqueNameValidationAsync(company.Name, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        // Crear Company y establecer valores de CompanySettings.
        company.CompanySettings.CompanyId = company.Id;
        company.CompanySettings.Timezone = timezone;

        await context.Companies.AddAsync(company, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return company;
    }
}
