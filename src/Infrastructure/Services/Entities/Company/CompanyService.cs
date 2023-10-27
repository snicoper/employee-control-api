using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Company;
using EmployeeControl.Infrastructure.Data;

namespace EmployeeControl.Infrastructure.Services.Entities.Company;

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
        await companyValidatorService.UniqueNameValidationAsync(company.Name.ToEmptyIfNull(), cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        await context.Companies.AddAsync(company, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return company;
    }
}
