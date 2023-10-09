using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Validators;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

public class IdentityRegisterValidator : AbstractValidator<IdentityRegisterCommand>
{
    public IdentityRegisterValidator(
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        RuleFor(r => r.UserName)
            .NotEmpty()
            .MaximumLength(256)
            .IdentityUniqueUserName(userManager, localizer);

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .IdentityUniqueEmail(userManager, localizer);

        RuleFor(r => r.Password)
            .NotEmpty()
            .Equal(r => r.ConfirmPassword);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .Equal(r => r.Password);

        RuleFor(r => r.CompanyName)
            .NotEmpty()
            .MaximumLength(50)
            .CompanyUniqueName(context, localizer);
    }
}
