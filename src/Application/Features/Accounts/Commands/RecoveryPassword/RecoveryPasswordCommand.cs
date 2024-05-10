using EmployeeControl.Application.Common.Interfaces.Messaging;

namespace EmployeeControl.Application.Features.Accounts.Commands.RecoveryPassword;

public record RecoveryPasswordCommand(string Email) : ICommand;
