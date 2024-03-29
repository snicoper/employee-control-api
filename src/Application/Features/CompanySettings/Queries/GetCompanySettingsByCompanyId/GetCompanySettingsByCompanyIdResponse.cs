using AutoMapper;

namespace EmployeeControl.Application.Features.CompanySettings.Queries.GetCompanySettingsByCompanyId;

public record GetCompanySettingsByCompanyIdResponse(
    string Id,
    string Timezone,
    int MaximumDailyWorkHours,
    bool GeolocationRequired)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.CompanySettings, GetCompanySettingsByCompanyIdResponse>();
        }
    }
}
