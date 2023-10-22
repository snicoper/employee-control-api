using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employee.Commands.InviteEmployee;

public record InviteEmployeeCommand(string FirstName, string LastName, string Email, int CompanyId) : IRequest<Result>;
