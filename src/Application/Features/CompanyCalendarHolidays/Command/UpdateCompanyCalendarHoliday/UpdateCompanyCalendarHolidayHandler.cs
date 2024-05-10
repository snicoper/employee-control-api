using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendarHolidays;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.UpdateCompanyCalendarHoliday;

internal class UpdateCompanyCalendarHolidayHandler(ICompanyCalendarHolidaysService companyCalendarHolidaysService)
    : ICommandHandler<UpdateCompanyCalendarHolidayCommand>
{
    public async Task<Result> Handle(UpdateCompanyCalendarHolidayCommand request, CancellationToken cancellationToken)
    {
        var companyHoliday = await companyCalendarHolidaysService.GetByIdAsync(request.Id, cancellationToken);
        companyHoliday.Description = request.Description;
        await companyCalendarHolidaysService.UpdateAsync(companyHoliday, cancellationToken);

        return Result.Success();
    }
}
