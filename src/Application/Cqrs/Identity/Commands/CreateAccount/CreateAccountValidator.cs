using FluentValidation;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.CreateAccount;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
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
