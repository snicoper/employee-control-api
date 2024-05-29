using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Companies;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Features.Companies;

public class CompanyValidatorService(
    IApplicationDbContext context,
    ILogger<CompanyValidatorService> logger,
    IStringLocalizer<CompanyResource> localizer)
    : ICompanyValidatorService
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
