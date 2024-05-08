﻿using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Companies;
using EmployeeControl.Application.Common.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Features.Companies;

public class CompanyValidatorService(
    IApplicationDbContext context,
    IValidationFailureService validationFailureService,
    ILogger<CompanyValidatorService> logger,
    IStringLocalizer<CompanyResource> localizer)
    : ICompanyValidatorService
{
    public async Task UniqueNameValidationAsync(string companyName, CancellationToken cancellationToken)
    {
        var company = await context
            .Companies
            .AnyAsync(c => string.Equals(c.Name.ToLower(), companyName.ToLower()), cancellationToken);

        if (!company)
        {
            return;
        }

        var message = localizer["El nombre de compañía ya existe."];
        logger.LogDebug("{message}", message);
        validationFailureService.Add("CompanyName", message);
    }
}
