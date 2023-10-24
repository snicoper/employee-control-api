using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdResponse
{
    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public bool Active { get; set; }

    public bool EmailConfirmed { get; set; }

    public ICollection<string> UserRoles { get; set; } = new List<string>();

    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, GetEmployeeByIdResponse>();
        }
    }
}
