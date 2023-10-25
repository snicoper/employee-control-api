﻿using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;

internal class GetEmployeesPaginatedHandler(
        IIdentityService identityService,
        ICurrentUserService currentUserService,
        IMapper mapper)
    : IRequestHandler<GetEmployeesPaginatedQuery, ResponseData<GetEmployeesPaginatedResponse>>
{
    public async Task<ResponseData<GetEmployeesPaginatedResponse>> Handle(
        GetEmployeesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var companyId = currentUserService.CompanyId;
        var users = identityService.GetAccountsByCompanyId(companyId);

        var resultResponse = await ResponseData<GetEmployeesPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}