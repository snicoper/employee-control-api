using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

internal class GetCompanyTasksByIdHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetCompanyTasksByIdQuery, GetCompanyTasksByIdResponse>
{
    public Task<GetCompanyTasksByIdResponse> Handle(GetCompanyTasksByIdQuery request, CancellationToken cancellationToken)
    {
        var companyTask = context.CompanyTasks.SingleOrDefault(ct => ct.Id == request.Id) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        var result = mapper.Map<CompanyTask, GetCompanyTasksByIdResponse>(companyTask);

        return Task.FromResult(result);
    }
}
