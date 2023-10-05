using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace EmployeeControl.Application.Features.Localization.Queries.CurrentLocale;

public class CurrentLocaleHandler : IRequestHandler<CurrentLocaleQuery, CurrentLocaleDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentLocaleHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<CurrentLocaleDto> Handle(CurrentLocaleQuery request, CancellationToken cancellationToken)
    {
        var culture = _httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>();
        var locale = culture?.RequestCulture.Culture.ToString();
        var result = new CurrentLocaleDto { Locale = locale };

        return Task.FromResult(result);
    }
}
