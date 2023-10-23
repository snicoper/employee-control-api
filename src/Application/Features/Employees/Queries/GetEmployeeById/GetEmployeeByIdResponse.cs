using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    bool Active,
    bool EmailConfirmed)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetEmployeeByIdResponse>();
        }
    }
}
