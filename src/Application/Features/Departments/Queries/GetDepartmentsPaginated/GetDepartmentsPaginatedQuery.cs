﻿using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetDepartmentsPaginatedQuery(RequestData RequestData)
    : IQuery<ResponseData<GetDepartmentsPaginatedResponse>>;
