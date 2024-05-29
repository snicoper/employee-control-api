using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsByEmployeeIdPaginated;

public record GetDepartmentsByEmployeeIdPaginatedResponse(Guid Id, string Name, string Active, string Background, string Color)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, GetDepartmentsByEmployeeIdPaginatedResponse>();
        }
    }
}
