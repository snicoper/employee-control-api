﻿using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesByCompanyTaskIdPaginated;

internal class GetEmployeesByCompanyTaskIdPaginatedHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IIdentityService identityService)
    : IRequestHandler<GetEmployeesByCompanyTaskIdPaginatedQuery, ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>>
{
    public async Task<ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>> Handle(
        GetEmployeesByCompanyTaskIdPaginatedQuery request,
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

        var resultResponse = await ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
