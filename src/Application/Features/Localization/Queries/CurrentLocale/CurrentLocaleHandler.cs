using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace EmployeeControl.Application.Features.Localization.Queries.CurrentLocale;

internal class CurrentLocaleHandler(IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CurrentLocaleQuery, CurrentLocaleDto>
{
    public Task<CurrentLocaleDto> Handle(CurrentLocaleQuery request, CancellationToken cancellationToken)
    {
        var culture = httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>();
        var locale = culture?.RequestCulture.Culture.ToString();
        var result = new CurrentLocaleDto { Locale = locale };
        var resultResponse = Task.FromResult(result);

        return resultResponse;
    }
}
