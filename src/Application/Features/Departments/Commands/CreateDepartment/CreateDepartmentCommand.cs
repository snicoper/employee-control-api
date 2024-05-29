using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Departments.Commands.CreateDepartment;

[Authorize(Roles = Roles.HumanResources)]
public record CreateDepartmentCommand(string Name, string Background, string Color) : ICommand<Guid>
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateDepartmentCommand, Department>();
        }
    }
}
