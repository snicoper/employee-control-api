using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesByCompanyTaskIdPaginated;

internal class GetEmployeesByCompanyTaskIdPaginatedHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetEmployeesByCompanyTaskIdPaginatedQuery, ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>>> Handle(
        GetEmployeesByCompanyTaskIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = context
            .EmployeeCompanyTasks
            .Include(uct => uct.User)
            .Where(uct => uct.CompanyTaskId == request.CompanyTaskId)
            .Select(uct => uct.User);

        var resultResponse = await ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
