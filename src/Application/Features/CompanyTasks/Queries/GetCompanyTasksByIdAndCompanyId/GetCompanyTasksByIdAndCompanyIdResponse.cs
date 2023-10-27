using AutoMapper;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByIdAndCompanyId;

public record GetCompanyTasksByIdAndCompanyIdResponse(int Id, int CompanyId, string Name, bool Active, DateTimeOffset Created)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CompanyTask, GetCompanyTasksByIdResponse>();
        }
    }
}
