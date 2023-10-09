using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Common.Validators;

public static class IdentityValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IdentityUniqueUserName<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        return ruleBuilder
            .MustAsync(async (userName, cancellationToken) =>
            {
                return string.IsNullOrEmpty(userName) ||
                       !await userManager.Users.AnyAsync(u => u.UserName == userName, cancellationToken);
            })
            .WithMessage(localizer["El nombre de usuario ya esta en uso."]);
    }

    public static IRuleBuilderOptions<T, string> IdentityUniqueEmail<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        return ruleBuilder
            .MustAsync(async (email, cancellationToken) =>
            {
                return string.IsNullOrEmpty(email) ||
                       !await userManager.Users.AnyAsync(u => u.UserName == email, cancellationToken);
            })
            .WithMessage(localizer["El email ya esta en uso."]);
    }
}
