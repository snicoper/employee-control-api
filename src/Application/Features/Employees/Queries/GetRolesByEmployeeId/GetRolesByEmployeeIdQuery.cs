﻿using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

[Authorize(Roles = Roles.HumanResources)]
public record GetRolesByEmployeeIdQuery(string EmployeeId) : IQuery<ICollection<GetRolesByEmployeeIdResponse>>;
