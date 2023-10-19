using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

public record GetAdminIdentitiesPaginatedResponse
{
    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetAdminIdentitiesPaginatedResponse>();
        }
    }
}
