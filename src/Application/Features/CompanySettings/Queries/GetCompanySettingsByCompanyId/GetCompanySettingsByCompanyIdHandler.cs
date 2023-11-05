using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using MediatR;

namespace EmployeeControl.Application.Features.CompanySettings.Queries.GetCompanySettingsByCompanyId;

internal class GetCompanySettingsByCompanyIdHandler(
    ICompanySettingsService companySettingsService,
    IEntityValidationService entityValidationService,
    IMapper mapper)
    : IRequestHandler<GetCompanySettingsByCompanyIdQuery, GetCompanySettingsByCompanyIdResponse>
{
    public async Task<GetCompanySettingsByCompanyIdResponse> Handle(
        GetCompanySettingsByCompanyIdQuery request,
        CancellationToken cancellationToken)
    {
        var companySettings = await companySettingsService.GetByCompanyIdAsync(request.CompanyId, cancellationToken);

        await entityValidationService.CheckEntityCompanyIsOwnerAsync(companySettings);
        var responseResult = mapper.Map<GetCompanySettingsByCompanyIdResponse>(companySettings);

        return responseResult;
    }
}
