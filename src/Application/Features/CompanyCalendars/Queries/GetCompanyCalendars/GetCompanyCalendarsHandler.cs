using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendars;

internal class GetCompanyCalendarsHandler(ICompanyCalendarRepository companyCalendarRepository, IMapper mapper)
    : IQueryHandler<GetCompanyCalendarsQuery, ICollection<GetCompanyCalendarsResponse>>
{
    public async Task<Result<ICollection<GetCompanyCalendarsResponse>>> Handle(
        GetCompanyCalendarsQuery request,
        CancellationToken cancellationToken)
    {
        var companyCalendars = await companyCalendarRepository.GetAllAsync(cancellationToken);
        var resultResponse = mapper.Map<ICollection<GetCompanyCalendarsResponse>>(companyCalendars);

        return Result.Success(resultResponse);
    }
}
