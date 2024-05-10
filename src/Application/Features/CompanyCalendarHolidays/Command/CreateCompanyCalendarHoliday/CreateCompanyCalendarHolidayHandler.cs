using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendarHolidays;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.CreateCompanyCalendarHoliday;

internal class CreateCompanyCalendarHolidayHandler(ICompanyCalendarHolidaysService companyCalendarHolidaysService, IMapper mapper)
    : ICommandHandler<CreateCompanyCalendarHolidayCommand, string>
{
    public async Task<Result<string>> Handle(
        CreateCompanyCalendarHolidayCommand request,
        CancellationToken cancellationToken)
    {
        var companyHoliday = mapper.Map<CompanyCalendarHoliday>(request);
        companyHoliday = await companyCalendarHolidaysService.CreateAsync(companyHoliday, cancellationToken);
        var resultResponse = companyHoliday.Id;

        return Result.Success(resultResponse);
    }
}
