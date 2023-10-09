using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

public record IdentityRegisterCommand(
        string? UserName,
        string? Email,
        string? Password,
        string? ConfirmPassword,
        string? CompanyName)
    : IRequest<string>;
