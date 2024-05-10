using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeSettings;

[Authorize(Roles = Roles.Employee)]
public record UpdateEmployeeSettingsCommand(string Id, string UserId, string Timezone) : ICommand<EmployeeSettings>
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateEmployeeSettingsCommand, EmployeeSettings>();
        }
    }
}
