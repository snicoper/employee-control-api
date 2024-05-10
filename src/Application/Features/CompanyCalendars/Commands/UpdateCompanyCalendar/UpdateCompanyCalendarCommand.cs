using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.UpdateCompanyCalendar;

[Authorize(Roles = Roles.HumanResources)]
public record UpdateCompanyCalendarCommand(string Id, string Name, string Description) : ICommand
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateCompanyCalendarCommand, CompanyCalendar>();
        }
    }
}
