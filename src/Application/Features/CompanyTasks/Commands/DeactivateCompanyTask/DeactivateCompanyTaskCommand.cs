using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;

public record DeactivateCompanyTaskCommand(string CompanyTaskId) : IRequest<Unit>;
