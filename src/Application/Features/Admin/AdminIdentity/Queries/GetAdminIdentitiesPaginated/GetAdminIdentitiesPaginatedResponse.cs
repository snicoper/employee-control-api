using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

public record GetAdminIdentitiesPaginatedResponse(string Id, string FirstName, string LastName, string Email)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetAdminIdentitiesPaginatedResponse>();
        }
    }
}
