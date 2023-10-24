using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;

public record DeactivateEmployeeCommand(string EmployeeId) : IRequest<Result>;
