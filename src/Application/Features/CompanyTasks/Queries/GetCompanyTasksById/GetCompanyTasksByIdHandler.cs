using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

internal class GetCompanyTasksByIdHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IEntityValidationService entityValidationService)
    : IRequestHandler<GetCompanyTasksByIdQuery, GetCompanyTasksByIdResponse>
{
    public async Task<GetCompanyTasksByIdResponse> Handle(GetCompanyTasksByIdQuery request, CancellationToken cancellationToken)
    {
        var companyTask = context.CompanyTasks.SingleOrDefault(ct => ct.Id == request.Id) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        var result = mapper.Map<CompanyTask, GetCompanyTasksByIdResponse>(companyTask);

        return result;
    }
}
