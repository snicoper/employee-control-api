﻿using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Departments.Commands.ActivateDepartment;

[Authorize(Roles = Roles.HumanResources)]
public record ActivateDepartmentCommand(Guid DepartmentId) : ICommand;
