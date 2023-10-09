using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Localizations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Common.FluentValidators;

public static class CompanyValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> CompanyBeUniqueName<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IApplicationDbContext context,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        return ruleBuilder.MustAsync(async (companyName, cancellationToken) =>
            {
                return await context.Company.AllAsync(c => c.Name == companyName, cancellationToken);
            })
            .WithMessage(localizer["Nombre de compañía ya registrada."]);
    }
}
