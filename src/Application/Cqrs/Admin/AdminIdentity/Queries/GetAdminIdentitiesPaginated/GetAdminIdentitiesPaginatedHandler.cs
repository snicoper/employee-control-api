using AutoMapper;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Cqrs.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

public class GetAdminIdentitiesPaginatedHandler
    : IRequestHandler<GetAdminIdentitiesPaginatedQuery, ResponseData<GetAdminIdentitiesPaginatedDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAdminIdentitiesPaginatedHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ResponseData<GetAdminIdentitiesPaginatedDto>> Handle(
        GetAdminIdentitiesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = _userManager.Users.AsNoTracking();

        var result = await ResponseData<GetAdminIdentitiesPaginatedDto>.CreateAsync(
            users,
            request.RequestData,
            _mapper,
            cancellationToken);

        return result;
    }
}
