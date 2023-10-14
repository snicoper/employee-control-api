using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

public record IdentityRegisterCommand(
        string? FirstName,
        string? LastName,
        string? Email,
        string? Password,
        string? ConfirmPassword,
        string? CompanyName)
    : IRequest<string>;
