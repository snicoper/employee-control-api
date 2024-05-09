using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.CreateCompanyCalendar;

internal class CreateCompanyCalendarHandler(ICompanyCalendarsService companyCalendarsService, IMapper mapper)
    : ICommandHandler<CreateCompanyCalendarCommand, string>
{
    public async Task<Result<string>> Handle(CreateCompanyCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = mapper.Map<CompanyCalendar>(request);

        var result = await companyCalendarsService.CreateAsync(companyCalendar, cancellationToken);

        return Result.Success(result.Id);
    }
}
