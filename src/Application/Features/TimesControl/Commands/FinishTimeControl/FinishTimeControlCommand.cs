using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;

[Authorize(Roles = Roles.Employee)]
public record FinishTimeControlCommand(string EmployeeId) : IRequest<Result>;
