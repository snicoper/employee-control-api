using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

public record GetRolesByEmployeeIdResponse(Guid Id, string Name)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserRole, GetRolesByEmployeeIdResponse>();
        }
    }
}
