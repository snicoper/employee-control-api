using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Commands.ActivateEmployee;

public record ActivateEmployeeCommand(string EmployeeId) : IRequest<Result>;
