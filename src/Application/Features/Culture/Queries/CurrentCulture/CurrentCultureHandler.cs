using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace EmployeeControl.Application.Features.Culture.Queries.CurrentCulture;

public class CurrentCultureHandler : IRequestHandler<CurrentCultureQuery, CurrentCultureDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentCultureHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<CurrentCultureDto> Handle(CurrentCultureQuery request, CancellationToken cancellationToken)
    {
        var culture = _httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>();
        var locale = culture?.RequestCulture.Culture.ToString();
        var result = new CurrentCultureDto { Locale = locale };

        return Task.FromResult(result);
    }
}
