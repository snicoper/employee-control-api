using EmployeeControl.Application.Common.Interfaces.Messaging;

namespace EmployeeControl.Application.Features.Accounts.Commands.RestorePassword;

public record RestorePasswordCommand(string UserId, string Code, string Password, string ConfirmPassword)
    : ICommand;
