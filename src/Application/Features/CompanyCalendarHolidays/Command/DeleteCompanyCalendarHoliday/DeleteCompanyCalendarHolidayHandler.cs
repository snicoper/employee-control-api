using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.DeleteCompanyCalendarHoliday;

internal class DeleteCompanyCalendarHolidayHandler(ICompanyCalendarHolidaysRepository companyCalendarHolidaysRepository)
    : ICommandHandler<DeleteCompanyCalendarHolidayCommand>
{
    public async Task<Result> Handle(DeleteCompanyCalendarHolidayCommand request, CancellationToken cancellationToken)
    {
        var companyHoliday = await companyCalendarHolidaysRepository.GetByIdAsync(request.Id, cancellationToken);
        await companyCalendarHolidaysRepository.DeleteAsync(companyHoliday, cancellationToken);

        return Result.Success();
    }
}
