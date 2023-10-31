using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.TimeControl.Queries.GetTimeControlRangeByEmployeeId;

internal class GetTimeControlRangeByEmployeeIdHandler(
        IApplicationDbContext context,
        IEntityValidationService entityValidationService,
        IMapper mapper)
    : IRequestHandler<GetTimeControlRangeByEmployeeIdQuery, ICollection<GetTimeControlRangeByEmployeeIdResponse>>
{
    public async Task<ICollection<GetTimeControlRangeByEmployeeIdResponse>> Handle(
        GetTimeControlRangeByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = context
            .TimeControls
            .AsNoTracking()
            .Where(tc => tc.UserId == request.EmployeeId && tc.Start >= request.From && tc.Finish <= request.To);

        await entityValidationService.CheckEntityCompanyIsOwner(timeControl.First());

        var resultResponse = mapper.Map<ICollection<GetTimeControlRangeByEmployeeIdResponse>>(timeControl);

        return resultResponse;
    }
}
