using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployeeSettings;

public record GetCurrentEmployeeSettingsResponse(string Id, string TimeZone)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<EmployeeSettings, GetCurrentEmployeeSettingsResponse>();
        }
    }
}
