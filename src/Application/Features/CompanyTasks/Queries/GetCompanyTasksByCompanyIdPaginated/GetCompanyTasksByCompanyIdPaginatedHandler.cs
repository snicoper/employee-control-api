using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByCompanyIdPaginated;

internal class GetCompanyTasksByCompanyIdPaginatedHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    : IRequestHandler<GetCompanyTasksByCompanyIdPaginatedQuery, ResponseData<GetCompanyTasksByCompanyIdPaginatedResponse>>
{
    public async Task<ResponseData<GetCompanyTasksByCompanyIdPaginatedResponse>> Handle(
        GetCompanyTasksByCompanyIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        // Cada compañía solo puede ser sus tareas.
        if (currentUserService.CompanyId != request.CompanyId)
        {
            throw new UnauthorizedAccessException();
        }

        var tasks = context
            .CompanyTasks
            .Where(c => c.CompanyId == request.CompanyId)
            .AsNoTracking();

        var resultResponse = await ResponseData<GetCompanyTasksByCompanyIdPaginatedResponse>.CreateAsync(
            tasks,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
