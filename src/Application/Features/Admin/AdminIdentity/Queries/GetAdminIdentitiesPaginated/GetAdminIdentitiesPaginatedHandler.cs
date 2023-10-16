using AutoMapper;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

internal class GetAdminIdentitiesPaginatedHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    : IRequestHandler<GetAdminIdentitiesPaginatedQuery, ResponseData<GetAdminIdentitiesPaginatedResponse>>
{
    public async Task<ResponseData<GetAdminIdentitiesPaginatedResponse>> Handle(
        GetAdminIdentitiesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = userManager.Users.AsNoTracking();

        var resultResponse = await ResponseData<GetAdminIdentitiesPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
