using AutoMapper;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Departments.Commands.UpdateDepartment;

public record UpdateDepartmentCommand(string Id, string Name, string Background, string Color) : IRequest<Result>
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpdateDepartmentCommand, Department>();
        }
    }
}
