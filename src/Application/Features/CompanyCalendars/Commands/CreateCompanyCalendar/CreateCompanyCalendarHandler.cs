using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.CreateCompanyCalendar;

internal class CreateCompanyCalendarHandler(ICompanyCalendarRepository companyCalendarRepository, IMapper mapper)
    : ICommandHandler<CreateCompanyCalendarCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCompanyCalendarCommand request, CancellationToken cancellationToken)
    {
        var companyCalendar = mapper.Map<CompanyCalendar>(request);

        var result = await companyCalendarRepository.CreateAsync(companyCalendar, cancellationToken);

        return Result.Success(result.Id);
    }
}
