using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;

internal class DeactivateCompanyTaskHandler(IApplicationDbContext context, ICompanyTaskRepository companyTaskRepository)
    : ICommandHandler<DeactivateCompanyTaskCommand>
{
    public async Task<Result> Handle(DeactivateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskRepository.GetByIdAsync(request.CompanyTaskId, cancellationToken);
        companyTask.Active = false;

        context.CompanyTasks.Update(companyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
