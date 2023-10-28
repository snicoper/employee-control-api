using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

public record GetCompanyTasksByIdResponse(string Id, string CompanyId, string Name, bool Active, DateTimeOffset Created)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CompanyTask, GetCompanyTasksByIdResponse>();
        }
    }
}
