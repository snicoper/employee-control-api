using EmployeeControl.Application.Localizations;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Identity.Commands.RegisterIdentity;

public class RegisterIdentityValidator : AbstractValidator<RegisterIdentityCommand>
{
    public RegisterIdentityValidator(IStringLocalizer<IdentityLocalizer> localizer)
    {
        RuleFor(r => r.FirstName)
            .NotEmpty();

        RuleFor(r => r.LastName)
            .NotEmpty();

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

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
            .MaximumLength(50);
    }
}
