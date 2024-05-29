using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

internal class GetCompanyTasksByIdHandler(ICompanyTaskRepository companyTaskRepository, IMapper mapper)
    : IQueryHandler<GetCompanyTasksByIdQuery, GetCompanyTasksByIdResponse>
{
    public async Task<Result<GetCompanyTasksByIdResponse>> Handle(
        GetCompanyTasksByIdQuery request,
        CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskRepository.GetByIdAsync(request.Id, cancellationToken);
        var result = mapper.Map<CompanyTask, GetCompanyTasksByIdResponse>(companyTask);

        return Result.Success(result);
    }
}
