using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.UpdateCompanyTask;

internal class UpdateCompanyTaskHandler(
        IApplicationDbContext context,
        IEntityValidationService entityValidationService,
        IMapper mapper)
    : IRequestHandler<UpdateCompanyTaskCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await context
                              .CompanyTasks
                              .AsNoTracking()
                              .SingleOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        var updatedCompanyTask = mapper.Map(request, companyTask);

        context.CompanyTasks.Update(updatedCompanyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
