﻿using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Companies.Queries.GetCompanyByCurrentUser;

internal class GetCompanyByCurrentUserHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    : IRequestHandler<GetCompanyByCurrentUserQuery, GetCompanyByCurrentUserResponse>
{
    public Task<GetCompanyByCurrentUserResponse> Handle(GetCompanyByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var companyId = currentUserService.CompanyId;
        var company = context.Companies.SingleOrDefault(c => c.Id == companyId)
                      ?? throw new NotFoundException(nameof(Company), nameof(Company.Id));

        var result = new GetCompanyByCurrentUserResponse(company.Id, company.Name);

        return Task.FromResult(result);
    }
}
