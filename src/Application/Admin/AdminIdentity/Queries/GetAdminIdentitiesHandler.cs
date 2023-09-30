using AutoMapper;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Admin.AdminIdentity.Queries;

public class GetAdminIdentitiesHandler : IRequestHandler<GetAdminIdentitiesQuery, ResponseData<GetAdminIdentitiesDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAdminIdentitiesHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ResponseData<GetAdminIdentitiesDto>> Handle(
        GetAdminIdentitiesQuery request,
        CancellationToken cancellationToken)
    {
        var users = _userManager.Users.AsNoTracking();

        var result = await ResponseData<GetAdminIdentitiesDto>.CreateAsync(
            users,
            request.RequestData,
            _mapper,
            cancellationToken);

        return result;
    }
}
