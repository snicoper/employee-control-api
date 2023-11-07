using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByDepartmentIdPaginated;

internal class GetEmployeesByDepartmentIdPaginatedHandler(
    IApplicationDbContext context,
    IIdentityService identityService,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetEmployeesByDepartmentIdPaginatedQuery, ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>>
{
    public async Task<ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>> Handle(
        GetEmployeesByDepartmentIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var userDepartments = context
            .UserDepartments
            .Include(uct => uct.User)
            .Where(uct => uct.DepartmentId == request.DepartmentId);

        // Si Role no es al menos Staff, filtrar solo por tareas de la compañía del usuario actual.
        if (!await identityService.IsInRoleAsync(currentUserService.Id, Roles.SiteStaff))
        {
            userDepartments = userDepartments.Where(uct => uct.CompanyId == currentUserService.CompanyId);
        }

        var users = userDepartments
            .AsNoTracking()
            .Select(uct => uct.User);

        var resultResponse = await ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
