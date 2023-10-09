using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Localization;
using EmployeeControl.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Auth.Commands.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterValidator(
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityResource> localizer)
    {
        _context = context;
        _userManager = userManager;

        RuleFor(r => r.UserName)
            .NotEmpty()
            .MaximumLength(256)
            .MustAsync(BeUniqueUserName)
            .WithMessage(localizer["Nombre de usuario ya registrado"]);

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .MustAsync(BeUniqueEmail)
            .WithMessage(localizer["Email ya registrado"]);

        RuleFor(r => r.Password)
            .NotEmpty()
            .Equal(r => r.ConfirmPassword);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .Equal(r => r.Password);

        RuleFor(r => r.CompanyName)
            .NotEmpty()
            .MaximumLength(50)
            .MustAsync(BeUniqueCompanyName)
            .WithMessage(localizer["Nombre de compañía ya registrada"]);
    }

    public async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AllAsync(u => u.UserName == userName, cancellationToken);
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AllAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> BeUniqueCompanyName(string companyName, CancellationToken cancellationToken)
    {
        return await _context.Company.AllAsync(c => c.Name == companyName, cancellationToken);
    }
}
