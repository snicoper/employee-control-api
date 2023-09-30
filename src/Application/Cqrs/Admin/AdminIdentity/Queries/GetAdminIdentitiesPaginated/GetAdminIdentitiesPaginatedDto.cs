using AutoMapper;
using EmployeeControl.Application.Common.Mapping;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Cqrs.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

public class GetAdminIdentitiesPaginatedDto : IMapFrom<ApplicationUser>
{
    public string? Id { get; set; }

    public string? NombreDeUsuario { get; set; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<ApplicationUser, GetAdminIdentitiesPaginatedDto>()
            .ForMember(
                dest => dest.NombreDeUsuario,
                opt => opt.MapFrom(src => $"{src.UserName}, {src.UserName}"));
    }
}
