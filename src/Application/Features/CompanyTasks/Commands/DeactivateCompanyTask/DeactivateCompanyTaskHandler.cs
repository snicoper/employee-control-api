using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;

internal class DeactivateCompanyTaskHandler(IApplicationDbContext context, IEntityValidationService entityValidationService)
    : IRequestHandler<DeactivateCompanyTaskCommand, Unit>
{
    public async Task<Unit> Handle(DeactivateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await context
                              .CompanyTasks
                              .AsNoTracking()
                              .SingleOrDefaultAsync(ct => ct.Id == request.CompanyTaskId, cancellationToken) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        companyTask.Active = false;

        context.CompanyTasks.Update(companyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
