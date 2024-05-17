using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.UpdateCompanyCalendar;

internal sealed class UpdateCompanyCalendarHandler(ICompanyCalendarsRepository companyCalendarsRepository, IMapper mapper)
    : ICommandHandler<UpdateCompanyCalendarCommand>
{
    public async Task<Result> Handle(UpdateCompanyCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = await companyCalendarsRepository.GetByIdAsync(request.Id, cancellationToken);
        var updateCompanyCalendar = mapper.Map(request, companyCalendar);

        await companyCalendarsRepository.UpdateAsync(updateCompanyCalendar, cancellationToken);

        return Result.Success();
    }
}
