using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Common.FluentValidators;

public static class IdentityValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IdentityBeUniqueUserName<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        return ruleBuilder
            .MustAsync(async (userName, cancellationToken) =>
            {
                return await userManager.Users.AllAsync(u => u.UserName == userName, cancellationToken);
            })
            .WithMessage(localizer["El nombre de usuario ya esta en uso."]);
    }

    public static IRuleBuilderOptions<T, string> IdentityBeUniqueEmail<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        return ruleBuilder
            .MustAsync(async (email, cancellationToken) =>
            {
                return await userManager.Users.AllAsync(u => u.UserName == email, cancellationToken);
            })
            .WithMessage(localizer["El email ya esta en uso."]);
    }
}
