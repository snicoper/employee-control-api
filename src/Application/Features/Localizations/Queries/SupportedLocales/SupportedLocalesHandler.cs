using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Localizations.Queries.SupportedLocales;

internal class SupportedLocalesHandler : IQueryHandler<SupportedLocalesQuery, SupportedLocalesResponse>
{
    public Task<Result<SupportedLocalesResponse>> Handle(SupportedLocalesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = AppCultures
            .GetAll()
            .Select(cultureInfo => cultureInfo.Name)
            .ToList();

        var supportedLocalesResponse = new SupportedLocalesResponse(supportedCultures);

        return Task.FromResult(Result.Success(supportedLocalesResponse));
    }
}
