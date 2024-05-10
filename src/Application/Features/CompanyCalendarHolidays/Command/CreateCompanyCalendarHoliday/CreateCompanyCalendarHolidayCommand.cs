using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.CreateCompanyCalendarHoliday;

[Authorize(Roles = Roles.HumanResources)]
public record CreateCompanyCalendarHolidayCommand(DateOnly Date, string Description, string CompanyCalendarId)
    : ICommand<string>
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateCompanyCalendarHolidayCommand, CompanyCalendarHoliday>();
        }
    }
}
