using EmployeeControl.Application.Common.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Localization.Queries.SupportedLocales;

internal class SupportedLocalesHandler : IRequestHandler<SupportedLocalesQuery, SupportedLocalesDto>
{
    public Task<SupportedLocalesDto> Handle(SupportedLocalesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = AppCultures.GetAll().Select(cultureInfo => cultureInfo.Name).ToList();
        var resultResponse = Task.FromResult(new SupportedLocalesDto(supportedCultures));

        return resultResponse;
    }
}
