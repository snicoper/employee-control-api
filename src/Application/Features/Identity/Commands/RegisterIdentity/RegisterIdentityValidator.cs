using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Validators;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Identity.Commands.RegisterIdentity;

public class RegisterIdentityValidator : AbstractValidator<RegisterIdentityCommand>
{
    public RegisterIdentityValidator(
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        RuleFor(r => r.FirstName)
            .NotEmpty();

        RuleFor(r => r.LastName)
            .NotEmpty();

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .IdentityUniqueEmail(userManager, localizer);

        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Equal(r => r.ConfirmPassword)
            .WithMessage(localizer["Las contraseñas no coinciden."]);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .Equal(r => r.Password)
            .WithMessage(localizer["Las contraseñas no coinciden."]);

        RuleFor(r => r.CompanyName)
            .NotEmpty()
            .MaximumLength(50)
            .CompanyUniqueName(context, localizer);
    }
}
