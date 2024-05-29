using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.UpdateCompanyTask;

internal class UpdateCompanyTaskHandler(
    IApplicationDbContext context,
    ICompanyTaskRepository companyTaskRepository,
    IMapper mapper)
    : ICommandHandler<UpdateCompanyTaskCommand>
{
    public async Task<Result> Handle(UpdateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskRepository.GetByIdAsync(request.Id, cancellationToken);
        var updatedCompanyTask = mapper.Map(request, companyTask);

        context.CompanyTasks.Update(updatedCompanyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
