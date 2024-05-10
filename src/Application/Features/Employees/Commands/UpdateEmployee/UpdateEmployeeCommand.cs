using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployee;

[Authorize(Roles = Roles.HumanResources)]
public record UpdateEmployeeCommand(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string CompanyCalendarId,
    string? PhoneNumber,
    DateTimeOffset? EntryDate)
    : ICommand
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateEmployeeCommand, User>();
        }
    }
}
