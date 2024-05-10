using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendarHolidays;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.DeleteCompanyCalendarHoliday;

internal class DeleteCompanyCalendarHolidayHandler(ICompanyCalendarHolidaysService companyCalendarHolidaysService)
    : ICommandHandler<DeleteCompanyCalendarHolidayCommand>
{
    public async Task<Result> Handle(DeleteCompanyCalendarHolidayCommand request, CancellationToken cancellationToken)
    {
        var companyHoliday = await companyCalendarHolidaysService.GetByIdAsync(request.Id, cancellationToken);
        await companyCalendarHolidaysService.DeleteAsync(companyHoliday, cancellationToken);

        return Result.Success();
    }
}
