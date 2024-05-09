using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendarById;

internal class GetCompanyCalendarByIdHandler(ICompanyCalendarsService companyCalendarsService, IMapper mapper)
    : IQueryHandler<GetCompanyCalendarByIdQuery, GetCompanyCalendarByIdResponse>
{
    public async Task<Result<GetCompanyCalendarByIdResponse>> Handle(
        GetCompanyCalendarByIdQuery request,
        CancellationToken cancellationToken)
    {
        var companyCalendar = await companyCalendarsService.GetByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetCompanyCalendarByIdResponse>(companyCalendar);

        return Result.Success(resultResponse);
    }
}
