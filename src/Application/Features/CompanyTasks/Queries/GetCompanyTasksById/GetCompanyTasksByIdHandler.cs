using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

internal class GetCompanyTasksByIdHandler(
    ICompanyTaskService companyTaskService,
    IMapper mapper,
    IEntityValidationService entityValidationService)
    : IRequestHandler<GetCompanyTasksByIdQuery, GetCompanyTasksByIdResponse>
{
    public async Task<GetCompanyTasksByIdResponse> Handle(GetCompanyTasksByIdQuery request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskService.GetByIdAsync(request.Id, cancellationToken);

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        var result = mapper.Map<CompanyTask, GetCompanyTasksByIdResponse>(companyTask);

        return result;
    }
}
