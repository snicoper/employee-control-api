using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanySettings.Commands.UpdateCompanySettings;

internal class UpdateCompanySettingsHandler(ICompanySettingsRepository companySettingsRepository, IMapper mapper)
    : ICommandHandler<UpdateCompanySettingsCommand>
{
    public async Task<Result> Handle(UpdateCompanySettingsCommand request, CancellationToken cancellationToken)
    {
        var companySettings = await companySettingsRepository.GetByIdAsync(request.Id, cancellationToken);
        var companySettingsUpdated = mapper.Map(request, companySettings);

        await companySettingsRepository.UpdateAsync(companySettingsUpdated, cancellationToken);

        return Result.Success();
    }
}
