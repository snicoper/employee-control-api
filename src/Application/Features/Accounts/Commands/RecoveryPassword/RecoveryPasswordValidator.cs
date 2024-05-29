using FluentValidation;

namespace EmployeeControl.Application.Features.Accounts.Commands.RecoveryPassword;

internal class RecoveryPasswordValidator : AbstractValidator<RecoveryPasswordCommand>
{
    public RecoveryPasswordValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
