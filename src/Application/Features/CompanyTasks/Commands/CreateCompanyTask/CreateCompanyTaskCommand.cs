using AutoMapper;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;

public record CreateCompanyTaskCommand(int CompanyId, string Name) : IRequest<int>
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateCompanyTaskCommand, CompanyTask>();
        }
    }
}
