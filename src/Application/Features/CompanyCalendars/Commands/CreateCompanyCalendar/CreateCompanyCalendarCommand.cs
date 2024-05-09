using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.CreateCompanyCalendar;

[Authorize(Roles = Roles.HumanResources)]
public record CreateCompanyCalendarCommand(string Name, string Description, bool Default)
    : ICommand<string>
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateCompanyCalendarCommand, CompanyCalendar>();
        }
    }
}
