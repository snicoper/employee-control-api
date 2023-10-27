﻿using AutoMapper;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;

[Authorize(Roles = Roles.HumanResources)]
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
