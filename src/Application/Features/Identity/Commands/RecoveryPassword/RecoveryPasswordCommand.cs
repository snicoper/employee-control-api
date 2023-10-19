using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.RecoveryPassword;

public record RecoveryPasswordCommand(string Email) : IRequest<Result>;
