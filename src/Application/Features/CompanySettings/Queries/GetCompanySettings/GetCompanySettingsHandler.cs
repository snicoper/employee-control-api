using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CompanySettings.Queries.GetCompanySettings;

internal class GetCompanySettingsHandler(ICompanySettingsService companySettingsService, IMapper mapper)
    : IQueryHandler<GetCompanySettingsQuery, GetCompanySettingsResponse>
{
    public async Task<Result<GetCompanySettingsResponse>> Handle(
        GetCompanySettingsQuery request,
        CancellationToken cancellationToken)
    {
        var companySettings = await companySettingsService.GetCompanySettingsAsync(cancellationToken);
        var responseResult = mapper.Map<GetCompanySettingsResponse>(companySettings);

        return Result.Success(responseResult);
    }
}
