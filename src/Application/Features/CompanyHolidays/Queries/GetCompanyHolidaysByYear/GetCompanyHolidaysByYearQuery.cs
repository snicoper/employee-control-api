using MediatR;

namespace EmployeeControl.Application.Features.CompanyHolidays.Queries.GetCompanyHolidaysByYear;

public record GetCompanyHolidaysByYearQuery(int Year) : IRequest<List<GetCompanyHolidaysByYearResponse>>;
