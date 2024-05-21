using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

public record GetRolesByEmployeeIdResponse(string Id, string Name)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<IdentityRole, GetRolesByEmployeeIdResponse>();
        }
    }
}
