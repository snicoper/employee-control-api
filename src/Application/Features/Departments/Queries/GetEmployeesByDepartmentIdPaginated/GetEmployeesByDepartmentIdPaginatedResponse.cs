﻿using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByDepartmentIdPaginated;

public record GetEmployeesByDepartmentIdPaginatedResponse(Guid Id, string FirstName, string LastName, string Email)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetEmployeesByDepartmentIdPaginatedResponse>();
        }
    }
}
