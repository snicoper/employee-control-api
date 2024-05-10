using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Companies.Queries.GetCompanyByCurrentUser;

[Authorize(Roles = Roles.Employee)]
public record GetCompanyByCurrentUserQuery : IQuery<GetCompanyByCurrentUserResponse>;
