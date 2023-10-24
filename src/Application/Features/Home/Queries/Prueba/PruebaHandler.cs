using EmployeeControl.Application.Localizations;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Home.Queries.Prueba;

internal class PruebaHandler(IStringLocalizer<IdentityLocalizer> localizer, TimeProvider dateTime)
    : IRequestHandler<PruebaQuery, PruebaResponse>
{
    public Task<PruebaResponse> Handle(PruebaQuery request, CancellationToken cancellationToken)
    {
        var result = new PruebaResponse(localizer["Hello"], dateTime.GetLocalNow());
        var resultResponse = Task.FromResult(result);

        return resultResponse;
    }
}
