using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Entities.Company;
using EmployeeControl.Application.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Entities.Company;

public class CompanyValidatorService(
        IApplicationDbContext context,
        IValidationFailureService validationFailureService,
        ILogger<CompanyValidatorService> logger,
        IStringLocalizer<CompanyLocalizer> localizer)
    : ICompanyValidatorService
{
    public async Task UniqueNameValidationAsync(string name, CancellationToken cancellationToken)
    {
        var company = await context.Company.SingleOrDefaultAsync(c => c.Name == name, cancellationToken);

        if (company is not null)
        {
            var message = localizer["El nombre de compañía ya existe."];
            logger.LogDebug("{message}", message);
            validationFailureService.Add("CompanyName", message);
        }
    }
}
