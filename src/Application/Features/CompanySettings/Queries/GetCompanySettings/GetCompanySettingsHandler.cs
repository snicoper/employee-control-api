using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanySettings.Queries.GetCompanySettings;

internal class GetCompanySettingsHandler(ICompanySettingsRepository companySettingsRepository, IMapper mapper)
    : IQueryHandler<GetCompanySettingsQuery, GetCompanySettingsResponse>
{
    public async Task<Result<GetCompanySettingsResponse>> Handle(
        GetCompanySettingsQuery request,
        CancellationToken cancellationToken)
    {
        var companySettings = await companySettingsRepository.GetCompanySettingsAsync(cancellationToken);
        var responseResult = mapper.Map<GetCompanySettingsResponse>(companySettings);

        return Result.Success(responseResult);
    }
}
