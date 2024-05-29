using FluentValidation;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

internal class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(r => r.Password)
            .NotEmpty();
    }
}
