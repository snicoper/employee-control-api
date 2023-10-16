using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

[Authorize(Roles = Roles.Administrator)]
public record GetAdminIdentitiesPaginatedQuery(RequestData RequestData)
    : IRequest<ResponseData<GetAdminIdentitiesPaginatedResponse>>;
