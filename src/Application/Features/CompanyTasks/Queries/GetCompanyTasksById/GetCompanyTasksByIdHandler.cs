using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

internal class GetCompanyTasksByIdHandler(ICompanyTaskService companyTaskService, IMapper mapper)
    : IQueryHandler<GetCompanyTasksByIdQuery, GetCompanyTasksByIdResponse>
{
    public async Task<Result<GetCompanyTasksByIdResponse>> Handle(
        GetCompanyTasksByIdQuery request,
        CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskService.GetByIdAsync(request.Id, cancellationToken);
        var result = mapper.Map<CompanyTask, GetCompanyTasksByIdResponse>(companyTask);

        return Result.Success(result);
    }
}
