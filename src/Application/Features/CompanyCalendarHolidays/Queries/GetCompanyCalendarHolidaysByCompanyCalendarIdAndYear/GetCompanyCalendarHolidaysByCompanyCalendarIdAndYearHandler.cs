using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Queries.
    GetCompanyCalendarHolidaysByCompanyCalendarIdAndYear;

internal class GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearQuery,
        ICollection<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse>>
{
    public Task<Result<ICollection<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse>>> Handle(
        GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearQuery request,
        CancellationToken cancellationToken)
    {
        var companyHolidays = context
            .CompanyCalendarHoliday
            .Where(cch => cch.CompanyCalendarId == request.CompanyCalendarId && cch.Date.Year == request.Year)
            .OrderBy(cch => cch.Date);

        var resultResponse = mapper
            .Map<ICollection<CompanyCalendarHoliday>,
                ICollection<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse>>([.. companyHolidays]);

        return Task.FromResult(Result.Success(resultResponse));
    }
}
