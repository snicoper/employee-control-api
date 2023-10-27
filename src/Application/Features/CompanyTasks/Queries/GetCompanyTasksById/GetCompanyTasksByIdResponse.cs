using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

public record GetCompanyTasksByIdResponse(int Id, int CompanyId, string Name, bool Active, DateTimeOffset Created)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CompanyTask, GetCompanyTasksByIdResponse>();
        }
    }
}
