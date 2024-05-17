using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.CreateCompanyCalendar;

internal class CreateCompanyCalendarHandler(ICompanyCalendarRepository companyCalendarRepository, IMapper mapper)
    : ICommandHandler<CreateCompanyCalendarCommand, string>
{
    public async Task<Result<string>> Handle(CreateCompanyCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = mapper.Map<CompanyCalendar>(request);

        var result = await companyCalendarRepository.CreateAsync(companyCalendar, cancellationToken);

        return Result.Success(result.Id);
    }
}
