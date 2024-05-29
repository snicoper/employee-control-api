using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlWithEmployeeById;

[Authorize(Roles = Roles.HumanResources)]
public record GetTimeControlWithEmployeeByIdQuery(Guid Id)
    : IQuery<GetTimeControlWithEmployeeByIdResponse>;
