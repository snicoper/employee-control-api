using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksPaginated;

public record GetCompanyTasksPaginatedByCompanyIdResponse(
    string Id,
    string Name,
    string CompanyId,
    bool Active,
    DateTimeOffset Created)
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CompanyTask, GetCompanyTasksPaginatedByCompanyIdResponse>();
        }
    }
}
