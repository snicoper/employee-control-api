using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.UpdateCompanyCalendar;

internal sealed class UpdateCompanyCalendarHandler(ICompanyCalendarRepository companyCalendarRepository, IMapper mapper)
    : ICommandHandler<UpdateCompanyCalendarCommand>
{
    public async Task<Result> Handle(UpdateCompanyCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = await companyCalendarRepository.GetByIdAsync(request.Id, cancellationToken);
        var updateCompanyCalendar = mapper.Map(request, companyCalendar);

        await companyCalendarRepository.UpdateAsync(updateCompanyCalendar, cancellationToken);

        return Result.Success();
    }
}
