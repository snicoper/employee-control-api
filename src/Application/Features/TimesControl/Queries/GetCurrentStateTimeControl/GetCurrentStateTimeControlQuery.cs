using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetCurrentStateTimeControl;

[Authorize(Roles = Roles.Employee)]
public record GetCurrentStateTimeControlQuery(string EmployeeId) : IRequest<GetCurrentStateTimeControlResponse>;
