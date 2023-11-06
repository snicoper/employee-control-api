using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

[Authorize(Roles = Roles.EnterpriseStaff)]
public record UpdateEmployeeRolesCommand(string EmployeeId, IList<string> RolesToAdd) : IRequest<Result>;
