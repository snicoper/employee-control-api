using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlRangeByEmployeeId;

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
        var timeControlGroups = await context
            .TimeControls
            .AsNoTracking()
            .Where(tc => (tc.UserId == request.EmployeeId && tc.Start >= request.From && tc.Finish <= request.To) ||
                         (tc.Start >= request.From && tc.Finish == null))
            .GroupBy(tc => tc.Start.Day)
            .ToListAsync(cancellationToken);

        // Seleccionar el primer item para comprobar permisos de lectura.
        var firstTimeControl = timeControlGroups.FirstOrDefault()?.FirstOrDefault();

        if (firstTimeControl is null)
        {
            return new List<GetTimeControlRangeByEmployeeIdResponse>();
        }

        await entityValidationService.CheckEntityCompanyIsOwner(firstTimeControl!);

        var resultResponse = timeControlGroups.Select(group => new GetTimeControlRangeByEmployeeIdResponse
            {
                Day = group.Key,
                Times = mapper.Map<List<GetTimeControlRangeByEmployeeIdResponse.TimeControlResponse>>(group.ToList())
            })
            .ToList();

        return resultResponse;
    }
}
