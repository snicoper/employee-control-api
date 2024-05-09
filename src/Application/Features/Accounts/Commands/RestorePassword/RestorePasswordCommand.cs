using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Accounts.Commands.RestorePassword;

public record RestorePasswordCommand(string UserId, string Code, string Password, string ConfirmPassword)
    : IRequest<Result>;
