using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.RegisterIdentity;

public record RegisterIdentityCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword,
        string CompanyName)
    : IRequest<string>;
