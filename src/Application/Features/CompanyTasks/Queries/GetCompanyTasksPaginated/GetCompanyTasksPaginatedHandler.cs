using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localization;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksPaginated;

internal class GetCompanyTasksPaginatedHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IStringLocalizer<IdentityResource> localizer,
    ILogger<GetCompanyTasksPaginatedHandler> logger)
    : IRequestHandler<GetCompanyTasksPaginatedQuery, ResponseData<GetCompanyTasksPaginatedResponse>>
{
    public async Task<ResponseData<GetCompanyTasksPaginatedResponse>> Handle(
        GetCompanyTasksPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = context.CompanyTasks;

        var message = localizer["Hello"];
        logger.LogCritical(message);

        var resultResponse = await ResponseData<GetCompanyTasksPaginatedResponse>.CreateAsync(
            tasks,
            request.RequestData,
            mapper,
            cancellationToken);

        return resultResponse;
    }
}
