using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;

[Authorize(Roles = Roles.HumanResources)]
public record ActivateCompanyTaskCommand(string CompanyTaskId) : IRequest<Unit>;
