using FluentValidation;

namespace EmployeeControl.Application.Features.Identity.Commands.RecoveryPassword;

public class RecoveryPasswordValidator : AbstractValidator<RecoveryPasswordCommand>
{
    public RecoveryPasswordValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
