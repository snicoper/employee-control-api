using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Validators;

public class CompanyValidator(
    IApplicationDbContext context,
    ILogger<CompanyValidator> logger,
    IStringLocalizer<CompanyResource> localizer)
    : ICompanyValidator
{
    public async Task<Result> UniqueNameValidationAsync(string companyName, Result result, CancellationToken cancellationToken)
    {
        var company = await context
            .Companies
            .AnyAsync(c => string.Equals(c.Name.ToLower(), companyName.ToLower()), cancellationToken);

        if (!company)
        {
            return Result.Success();
        }

        var errorMessage = localizer["El nombre de compañía ya existe."];
        logger.LogDebug(errorMessage);
        result.AddError("CompanyName", errorMessage);

        return result;
    }
}
