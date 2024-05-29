using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.CreateCompanyCalendarHoliday;

internal class CreateCompanyCalendarHolidayHandler(
    ICompanyCalendarHolidayRepository companyCalendarHolidayRepository,
    IMapper mapper)
    : ICommandHandler<CreateCompanyCalendarHolidayCommand, string>
{
    public async Task<Result<string>> Handle(
        CreateCompanyCalendarHolidayCommand request,
        CancellationToken cancellationToken)
    {
        var companyHoliday = mapper.Map<CompanyCalendarHoliday>(request);
        companyHoliday = await companyCalendarHolidayRepository.CreateAsync(companyHoliday, cancellationToken);
        var resultResponse = companyHoliday.Id;

        return Result.Success(resultResponse);
    }
}
