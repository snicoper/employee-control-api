using EmployeeControl.Application.Localizations;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Home.Queries.Prueba;

internal class PruebaHandler(IStringLocalizer<IdentityLocalizer> localizer) : IRequestHandler<PruebaQuery, PruebaDto>
{
    public Task<PruebaDto> Handle(PruebaQuery request, CancellationToken cancellationToken)
    {
        var resultResponse = new PruebaDto { Message = localizer["Hello"] };

        return Task.FromResult(resultResponse);
    }
}
