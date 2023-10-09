using AutoMapper;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

internal class GetAdminIdentitiesPaginatedHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    : IRequestHandler<GetAdminIdentitiesPaginatedQuery, ResponseData<GetAdminIdentitiesPaginatedDto>>
{
    public async Task<ResponseData<GetAdminIdentitiesPaginatedDto>> Handle(
        GetAdminIdentitiesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = userManager.Users.AsNoTracking();

        var usersResponse = await ResponseData<GetAdminIdentitiesPaginatedDto>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return usersResponse;
    }
}
