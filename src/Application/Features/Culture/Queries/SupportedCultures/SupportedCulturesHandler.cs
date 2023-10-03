using EmployeeControl.Application.Common.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Culture.Queries.SupportedCultures;

public class SupportedCulturesHandler : IRequestHandler<SupportedCulturesQuery, SupportedCulturesDto>
{
    public Task<SupportedCulturesDto> Handle(SupportedCulturesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = new SupportedCulturesDto();

        foreach (var cultureInfo in AppCultures.GetAll())
        {
            supportedCultures.Locales.Add(cultureInfo.Name);
        }

        return Task.FromResult(supportedCultures);
    }
}
