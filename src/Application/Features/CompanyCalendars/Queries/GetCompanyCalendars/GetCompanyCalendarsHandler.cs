using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendars;

internal class GetCompanyCalendarsHandler(ICompanyCalendarsService companyCalendarsService, IMapper mapper)
    : IQueryHandler<GetCompanyCalendarsQuery, ICollection<GetCompanyCalendarsResponse>>
{
    public async Task<Result<ICollection<GetCompanyCalendarsResponse>>> Handle(
        GetCompanyCalendarsQuery request,
        CancellationToken cancellationToken)
    {
        var companyCalendars = await companyCalendarsService.GetAllAsync(cancellationToken);
        var resultResponse = mapper.Map<ICollection<GetCompanyCalendarsResponse>>(companyCalendars);

        return Result.Success(resultResponse);
    }
}
