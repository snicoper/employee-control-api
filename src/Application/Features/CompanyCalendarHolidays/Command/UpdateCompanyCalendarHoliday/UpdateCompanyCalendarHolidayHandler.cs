using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.UpdateCompanyCalendarHoliday;

internal class UpdateCompanyCalendarHolidayHandler(ICompanyCalendarHolidaysRepository companyCalendarHolidaysRepository)
    : ICommandHandler<UpdateCompanyCalendarHolidayCommand>
{
    public async Task<Result> Handle(UpdateCompanyCalendarHolidayCommand request, CancellationToken cancellationToken)
    {
        var companyHoliday = await companyCalendarHolidaysRepository.GetByIdAsync(request.Id, cancellationToken);
        companyHoliday.Description = request.Description;
        await companyCalendarHolidaysRepository.UpdateAsync(companyHoliday, cancellationToken);

        return Result.Success();
    }
}
