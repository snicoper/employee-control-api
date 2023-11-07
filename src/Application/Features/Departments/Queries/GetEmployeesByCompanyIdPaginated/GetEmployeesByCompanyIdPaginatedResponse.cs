using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByCompanyIdPaginated;

public record GetEmployeesByCompanyIdPaginatedResponse(string Id, string FirstName, string LastName, string Email)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetEmployeesByCompanyIdPaginatedResponse>();
        }
    }
}
