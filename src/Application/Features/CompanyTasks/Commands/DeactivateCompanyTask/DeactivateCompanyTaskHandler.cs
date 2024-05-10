using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;

internal class DeactivateCompanyTaskHandler(IApplicationDbContext context, ICompanyTaskService companyTaskService)
    : ICommandHandler<DeactivateCompanyTaskCommand>
{
    public async Task<Result> Handle(DeactivateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskService.GetByIdAsync(request.CompanyTaskId, cancellationToken);
        companyTask.Active = false;

        context.CompanyTasks.Update(companyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
