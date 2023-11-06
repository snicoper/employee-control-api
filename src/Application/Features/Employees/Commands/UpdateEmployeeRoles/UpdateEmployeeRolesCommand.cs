using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

public record UpdateEmployeeRolesCommand(string EmployeeId, IList<string> Roles) : IRequest<Result>;
