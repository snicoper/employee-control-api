using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByEmployeeIdPaginated;

internal class GetCompanyTasksByEmployeeIdPaginatedHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCompanyTasksByEmployeeIdPaginatedQuery, ResponseData<GetCompanyTasksByEmployeeIdPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetCompanyTasksByEmployeeIdPaginatedResponse>>> Handle(
        GetCompanyTasksByEmployeeIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var userCompanyTasks = context
            .EmployeeCompanyTasks
            .Include(uct => uct.CompanyTask)
            .Where(uct => uct.UserId == request.EmployeeId);

        var companyTasks = userCompanyTasks.Select(uct => uct.CompanyTask);

        var resultResponse = await ResponseData<GetCompanyTasksByEmployeeIdPaginatedResponse>.CreateAsync(
            companyTasks,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
