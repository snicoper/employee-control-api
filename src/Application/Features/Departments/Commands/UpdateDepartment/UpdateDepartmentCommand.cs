using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Departments.Commands.UpdateDepartment;

[Authorize(Roles = Roles.HumanResources)]
public record UpdateDepartmentCommand(Guid Id, string Name, string Background, string Color)
    : ICommand
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateDepartmentCommand, Department>();
        }
    }
}
