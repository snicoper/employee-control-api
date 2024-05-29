using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.UpdateCompanyCalendarHoliday;

internal class UpdateCompanyCalendarHolidayHandler(ICompanyCalendarHolidayRepository companyCalendarHolidayRepository)
    : ICommandHandler<UpdateCompanyCalendarHolidayCommand>
{
    public async Task<Result> Handle(UpdateCompanyCalendarHolidayCommand request, CancellationToken cancellationToken)
    {
        var companyHoliday = await companyCalendarHolidayRepository.GetByIdAsync(request.Id, cancellationToken);
        companyHoliday.Description = request.Description;
        await companyCalendarHolidayRepository.UpdateAsync(companyHoliday, cancellationToken);

        return Result.Success();
    }
}
