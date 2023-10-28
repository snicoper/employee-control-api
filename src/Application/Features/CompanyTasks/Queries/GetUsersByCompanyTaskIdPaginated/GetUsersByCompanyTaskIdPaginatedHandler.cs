using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetUsersByCompanyTaskIdPaginated;

internal class GetUsersByCompanyTaskIdPaginatedHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IIdentityService identityService)
    : IRequestHandler<GetUsersByCompanyTaskIdPaginatedQuery, ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>>
{
    public async Task<ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>> Handle(
        GetUsersByCompanyTaskIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var userCompanyTasks = context
            .UserCompanyTasks
            .Include(uct => uct.User)
            .Where(uct => uct.CompanyTaskId == request.CompanyTaskId);

        // Si Role no es al menos Staff, filtrar solo por tareas de la compañía del usuario actual.
        if (!await identityService.IsInRoleAsync(currentUserService.Id, Roles.Staff))
        {
            userCompanyTasks = userCompanyTasks.Where(uct => uct.CompanyId == currentUserService.CompanyId);
        }

        var users = userCompanyTasks
            .AsNoTracking()
            .Select(uct => uct.User);

        var resultResponse = await ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
