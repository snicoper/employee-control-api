using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;

internal class ActivateCompanyTaskHandler(IApplicationDbContext context, IEntityValidationService entityValidationService)
    : IRequestHandler<ActivateCompanyTaskCommand, Result>
{
    public async Task<Result> Handle(ActivateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await context
                              .CompanyTasks
                              .AsNoTracking()
                              .SingleOrDefaultAsync(ct => ct.Id == request.CompanyTaskId, cancellationToken) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        companyTask.Active = true;

        context.CompanyTasks.Update(companyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
