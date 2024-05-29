using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendarById;

internal class GetCompanyCalendarByIdHandler(ICompanyCalendarRepository companyCalendarRepository, IMapper mapper)
    : IQueryHandler<GetCompanyCalendarByIdQuery, GetCompanyCalendarByIdResponse>
{
    public async Task<Result<GetCompanyCalendarByIdResponse>> Handle(
        GetCompanyCalendarByIdQuery request,
        CancellationToken cancellationToken)
    {
        var companyCalendar = await companyCalendarRepository.GetByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetCompanyCalendarByIdResponse>(companyCalendar);

        return Result.Success(resultResponse);
    }
}
