using EmployeeControl.Application.Common.Interfaces.Entities.Company;
using EmployeeControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Entities.Company;

public class CompanyValidatorService(ApplicationDbContext context, ValidationFailureService validationFailureService)
    : ICompanyValidatorService
{
    public async Task UniqueNameValidationAsync(string name, CancellationToken cancellationToken)
    {
        var company = await context.Company.SingleOrDefaultAsync(c => c.Name == name, cancellationToken);

        if (company is not null)
        {
            validationFailureService.Add(nameof(company.Name), "El nombre de compañía ya existe.");
        }
    }
}
