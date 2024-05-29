using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanySettings.Commands.UpdateCompanySettings;

[Authorize(Roles = Roles.Staff)]
public record UpdateCompanySettingsCommand(
    Guid Id,
    string Timezone,
    int PeriodTimeControlMax,
    int WeeklyWorkingHours,
    bool GeolocationRequired)
    : ICommand
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateCompanySettingsCommand, Domain.Entities.CompanySettings>();
        }
    }
}
