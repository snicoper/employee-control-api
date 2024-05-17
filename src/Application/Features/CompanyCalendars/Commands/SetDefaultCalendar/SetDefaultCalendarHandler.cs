using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.SetDefaultCalendar;

internal class SetDefaultCalendarHandler(ICompanyCalendarRepository companyCalendarRepository)
    : ICommandHandler<SetDefaultCalendarCommand>
{
    public async Task<Result> Handle(SetDefaultCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = await companyCalendarRepository.GetByIdAsync(request.Id, cancellationToken);
        await companyCalendarRepository.SetDefaultCalendarAsync(companyCalendar, cancellationToken);

        return Result.Success();
    }
}
