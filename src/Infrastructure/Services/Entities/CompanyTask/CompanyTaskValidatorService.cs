using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities.CompanyTask;
using EmployeeControl.Application.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Entities.CompanyTask;

public class CompanyTaskValidatorService(
        IApplicationDbContext context,
        IValidationFailureService validationFailureService,
        IStringLocalizer<CompanyTaskLocalizer> localizer)
    : ICompanyTaskValidatorService
{
    public void ValidateCompanyName(Domain.Entities.CompanyTask companyTask)
    {
        var companyTaskExists = context
            .CompanyTasks
            .Where(ct => ct.CompanyId == companyTask.CompanyId && ct.Name == companyTask.Name)
            .AsNoTracking();

        if (!companyTaskExists.Any())
        {
            return;
        }

        var message = localizer["El nombre de compañía ya existe."];
        validationFailureService.Add(nameof(Domain.Entities.CompanyTask.Name), message);
    }
}
