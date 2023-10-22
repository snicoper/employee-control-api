using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;

public record GetEmployeesPaginatedResponse(string? FirstName, string? LastName, string? Email)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetEmployeesPaginatedResponse>();
        }
    }
}
