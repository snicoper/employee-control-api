using EmployeeControl.Application.Localization;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Home.Queries.Prueba;

public class PruebaHandler : IRequestHandler<PruebaQuery, PruebaDto>
{
    private readonly IStringLocalizer<IdentityResource> _localizer;

    public PruebaHandler(IStringLocalizer<IdentityResource> localizer)
    {
        _localizer = localizer;
    }

    public Task<PruebaDto> Handle(PruebaQuery request, CancellationToken cancellationToken)
    {
        var result = new PruebaDto { Message = _localizer["Hello"] };

        return Task.FromResult(result);
    }
}
