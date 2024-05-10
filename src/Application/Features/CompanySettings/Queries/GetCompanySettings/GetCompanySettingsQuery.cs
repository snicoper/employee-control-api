using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanySettings.Queries.GetCompanySettings;

[Authorize(Roles = Roles.Employee)]
public record GetCompanySettingsQuery : IQuery<GetCompanySettingsResponse>;
