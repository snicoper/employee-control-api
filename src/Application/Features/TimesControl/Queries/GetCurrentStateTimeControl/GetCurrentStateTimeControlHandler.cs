using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetCurrentStateTimeControl;

internal class GetCurrentStateTimeControlHandler(IApplicationDbContext context, IEntityValidationService entityValidationService)
    : IRequestHandler<GetCurrentStateTimeControlQuery, GetCurrentStateTimeControlResponse>
{
    public async Task<GetCurrentStateTimeControlResponse> Handle(
        GetCurrentStateTimeControlQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = await context
            .TimeControls
            .AsNoTracking()
            .SingleOrDefaultAsync(ct => ct.ClosedBy == ClosedBy.Unclosed, cancellationToken);

        var isOpen = timeControl is not null;

        if (timeControl is not null)
        {
            await entityValidationService.CheckEntityCompanyIsOwner(timeControl);
        }

        var resultResponse = new GetCurrentStateTimeControlResponse(isOpen);

        return resultResponse;
    }
}
