﻿using EmployeeControl.Application.Common.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Localization.Queries.SupportedLocales;

internal class SupportedLocalesHandler : IRequestHandler<SupportedLocalesQuery, SupportedLocalesResponse>
{
    public Task<SupportedLocalesResponse> Handle(SupportedLocalesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = AppCultures.GetAll().Select(cultureInfo => cultureInfo.Name).ToList();
        var resultResponse = Task.FromResult(new SupportedLocalesResponse(supportedCultures));

        return resultResponse;
    }
}
