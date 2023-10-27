using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByIdAndCompanyId;

internal class GetCompanyTasksByIdAndCompanyIdHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    : IRequestHandler<GetCompanyTasksByIdAndCompanyIdQuery, GetCompanyTasksByIdAndCompanyIdResponse>
{
    public async Task<GetCompanyTasksByIdAndCompanyIdResponse> Handle(
        GetCompanyTasksByIdAndCompanyIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.CompanyId != currentUserService.CompanyId)
        {
            throw new UnauthorizedAccessException();
        }

        var companyTask = await context.CompanyTasks.AsNoTracking()
                              .SingleOrDefaultAsync(
                                  ct => ct.Id == request.Id && ct.CompanyId == request.CompanyId,
                                  cancellationToken) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        var resultResponse = mapper.Map<CompanyTask, GetCompanyTasksByIdAndCompanyIdResponse>(companyTask);

        return resultResponse;
    }
}
