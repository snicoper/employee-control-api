using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.UpdateCompanyTask;

[Authorize(Roles = Roles.HumanResources)]
public record UpdateCompanyTaskCommand(Guid Id, string Name, string Background, string Color, bool Active) : ICommand
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateCompanyTaskCommand, CompanyTask>();
        }
    }
}
