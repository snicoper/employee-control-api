using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.CompanySettings.Commands.UpdateCompanySettings;

public class UpdateCompanySettingsHandler(
    ICompanySettingsService companySettingsService,
    IEntityValidationService entityValidationService,
    IMapper mapper)
    : IRequestHandler<UpdateCompanySettingsCommand, Result>
{
    public async Task<Result> Handle(UpdateCompanySettingsCommand request, CancellationToken cancellationToken)
    {
        var companySettings = await companySettingsService.GatById(request.Id, cancellationToken);

        await entityValidationService.CheckEntityCompanyIsOwner(companySettings);

        var companySettingsUpdated = mapper.Map(request, companySettings);

        await companySettingsService.UpdateAsync(companySettingsUpdated, cancellationToken);

        return Result.Success();
    }
}
