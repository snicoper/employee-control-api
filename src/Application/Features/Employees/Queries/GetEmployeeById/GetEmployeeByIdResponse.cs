using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdResponse(
    string Id,
    int CompanyId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    DateTimeOffset? EntryDate,
    bool Active,
    bool EmailConfirmed)
{
    public ICollection<string> UserRoles { get; set; } = new List<string>();

    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetEmployeeByIdResponse>();
        }
    }
}
