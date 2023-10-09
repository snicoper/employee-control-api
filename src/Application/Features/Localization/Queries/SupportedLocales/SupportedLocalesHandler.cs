using EmployeeControl.Application.Common.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Localization.Queries.SupportedLocales;

internal class SupportedLocalesHandler : IRequestHandler<SupportedLocalesQuery, SupportedLocalesDto>
{
    public Task<SupportedLocalesDto> Handle(SupportedLocalesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = new SupportedLocalesDto();

        foreach (var cultureInfo in AppCultures.GetAll())
        {
            supportedCultures.Locales.Add(cultureInfo.Name);
        }

        var resultResponse = Task.FromResult(supportedCultures);

        return resultResponse;
    }
}
