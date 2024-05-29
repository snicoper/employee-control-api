using EmployeeControl.Application.Common.Interfaces.Messaging;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;
