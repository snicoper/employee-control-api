using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Admin.AdminIdentity.Queries;

public class GetAdminIdentitiesQuery : IRequest<ResponseData<GetAdminIdentitiesDto>>
{
    public GetAdminIdentitiesQuery(RequestData requestData)
    {
        RequestData = requestData;
    }

    public RequestData RequestData { get; }
}
