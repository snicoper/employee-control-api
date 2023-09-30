using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Cqrs.Admin.AdminIdentity.Queries;

[Authorize(Roles = Roles.Administrator)]
public class GetAdminIdentitiesQuery : IRequest<ResponseData<GetAdminIdentitiesDto>>
{
    public GetAdminIdentitiesQuery(RequestData requestData)
    {
        RequestData = requestData;
    }

    public RequestData RequestData { get; }
}
