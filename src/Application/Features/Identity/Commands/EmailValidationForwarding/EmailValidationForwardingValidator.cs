using FluentValidation;

namespace EmployeeControl.Application.Features.Identity.Commands.EmailValidationForwarding;

public class EmailValidationForwardingValidator : AbstractValidator<EmailValidationForwardingCommand>
{
    public EmailValidationForwardingValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty();
    }
}
