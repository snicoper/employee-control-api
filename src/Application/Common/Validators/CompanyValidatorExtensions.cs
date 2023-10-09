using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Localizations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Common.Validators;

public static class CompanyValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> CompanyUniqueName<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IApplicationDbContext context,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        return ruleBuilder.MustAsync(async (companyName, cancellationToken) =>
            {
                return string.IsNullOrEmpty(companyName) ||
                       !await context.Company.AnyAsync(c => c.Name == companyName, cancellationToken);
            })
            .WithMessage(localizer["Nombre de compañía ya registrada."]);
    }
}
