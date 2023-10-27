using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksPaginated;

internal class GetCompanyTasksPaginatedByCompanyIdHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService currentUserService)
    : IRequestHandler<GetCompanyTasksPaginatedByCompanyIdQuery, ResponseData<GetCompanyTasksPaginatedByCompanyIdResponse>>
{
    public async Task<ResponseData<GetCompanyTasksPaginatedByCompanyIdResponse>> Handle(
        GetCompanyTasksPaginatedByCompanyIdQuery request,
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

        var resultResponse = await ResponseData<GetCompanyTasksPaginatedByCompanyIdResponse>.CreateAsync(
            tasks,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
