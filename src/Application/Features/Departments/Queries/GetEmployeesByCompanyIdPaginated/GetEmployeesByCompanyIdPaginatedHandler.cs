using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByCompanyIdPaginated;

internal class GetEmployeesByCompanyIdPaginatedHandler(
    IApplicationDbContext context,
    IPermissionsValidationService permissionsValidationService,
    IMapper mapper)
    : IRequestHandler<GetEmployeesByCompanyIdPaginatedQuery, ResponseData<GetEmployeesByCompanyIdPaginatedResponse>>
{
    public async Task<ResponseData<GetEmployeesByCompanyIdPaginatedResponse>> Handle(
        GetEmployeesByCompanyIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        // Cada compañía solo puede ser sus tareas.
        permissionsValidationService.ItsFromTheCompany(request.CompanyId);

        var tasks = context
            .CompanyTasks
            .Where(c => c.CompanyId == request.CompanyId)
            .AsNoTracking();

        var resultResponse = await ResponseData<GetEmployeesByCompanyIdPaginatedResponse>.CreateAsync(
            tasks,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
