using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.SetDefaultCalendar;

internal class SetDefaultCalendarHandler(ICompanyCalendarsRepository companyCalendarsRepository)
    : ICommandHandler<SetDefaultCalendarCommand>
{
    public async Task<Result> Handle(SetDefaultCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = await companyCalendarsRepository.GetByIdAsync(request.Id, cancellationToken);
        await companyCalendarsRepository.SetDefaultCalendarAsync(companyCalendar, cancellationToken);

        return Result.Success();
    }
}
