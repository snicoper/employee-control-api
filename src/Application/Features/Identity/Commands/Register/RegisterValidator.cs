using FluentValidation;

namespace EmployeeControl.Application.Features.Identity.Commands.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty()
            .NotNull();

        RuleFor(r => r.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull()
            .Equal(r => r.ConfirmPassword);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .Equal(r => r.Password);
    }
}
