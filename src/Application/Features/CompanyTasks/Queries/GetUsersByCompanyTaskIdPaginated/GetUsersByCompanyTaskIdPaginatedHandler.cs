using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetUsersByCompanyTaskIdPaginated;

internal class GetUsersByCompanyTaskIdPaginatedHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetUsersByCompanyTaskIdPaginatedQuery, ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>>
{
    public async Task<ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>> Handle(
        GetUsersByCompanyTaskIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = context
            .UserCompanyTasks
            .Include(uct => uct.User)
            .Where(uct => uct.CompanyTaskId == request.CompanyTaskId)
            .AsNoTracking()
            .Select(uct => uct.User);

        var resultResponse = await ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
