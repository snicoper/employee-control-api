using AutoMapper;
using EmployeeControl.Application.Common.Mapping;
using EmployeeControl.Domain.Entities.Identity;

namespace EmployeeControl.Application.Admin.AdminIdentity.Queries;

public class GetAdminIdentitiesDto : IMapFrom<ApplicationUser>
{
    public string? Id { get; set; }

    public string? NombreDeUsuario { get; set; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<ApplicationUser, GetAdminIdentitiesDto>()
            .ForMember(
                dest => dest.NombreDeUsuario,
                opt => opt.MapFrom(src => $"{src.UserName}, {src.UserName}"));
    }
}
