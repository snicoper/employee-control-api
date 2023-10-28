using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetUsersByCompanyTaskIdPaginated;

public record GetUsersByCompanyTaskIdPaginatedResponse(string Id, string FirstName, string LastName, string Email)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ApplicationUser, GetUsersByCompanyTaskIdPaginatedResponse>();
        }
    }
}
