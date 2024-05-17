using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;

internal class ActivateCompanyTaskHandler(IApplicationDbContext context, ICompanyTaskRepository companyTaskRepository)
    : ICommandHandler<ActivateCompanyTaskCommand>
{
    public async Task<Result> Handle(ActivateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskRepository.GetByIdAsync(request.CompanyTaskId, cancellationToken);
        companyTask.Active = true;

        context.CompanyTasks.Update(companyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
