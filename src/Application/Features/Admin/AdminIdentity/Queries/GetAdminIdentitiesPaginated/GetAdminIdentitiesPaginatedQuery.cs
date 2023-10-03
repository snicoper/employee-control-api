using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;

[Authorize(Roles = Roles.Administrator)]
public class GetAdminIdentitiesPaginatedQuery : IRequest<ResponseData<GetAdminIdentitiesPaginatedDto>>
{
    public GetAdminIdentitiesPaginatedQuery(RequestData requestData)
    {
        RequestData = requestData;
    }

    public RequestData RequestData { get; }
}
