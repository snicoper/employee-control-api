using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;

public record ActivateCompanyTaskCommand(string CompanyTaskId) : IRequest<Unit>;
