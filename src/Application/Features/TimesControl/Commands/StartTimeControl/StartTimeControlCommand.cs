using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

[Authorize(Roles = Roles.Employee)]
public record StartTimeControlCommand(string EmployeeId) : IRequest<Result>;
