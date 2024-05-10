﻿using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlById;

[Authorize(Roles = Roles.HumanResources)]
public record GetTimeControlByIdQuery(string Id) : IQuery<GetTimeControlByIdResponse>;
