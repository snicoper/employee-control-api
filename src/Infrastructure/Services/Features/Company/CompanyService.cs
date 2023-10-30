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
    public async Task<Domain.Entities.Company> CreateCompanyAsync(
        Domain.Entities.Company company,
        CancellationToken cancellationToken)
    {
        // Validaciones.
        await companyValidatorService.UniqueNameValidationAsync(company.Name, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        await context.Companies.AddAsync(company, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return company;
    }
}
