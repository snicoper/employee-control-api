using FluentValidation;

namespace EmployeeControl.Application.Features.Accounts.Commands.EmailValidationForwarding;

public class EmailValidationForwardingValidator : AbstractValidator<EmailValidationForwardingCommand>
{
    public EmailValidationForwardingValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty();
    }
}
