using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.CompanyCalendars;

public class CompanyCalendarsService(IApplicationDbContext context) : ICompanyCalendarsService
{
    public async Task<ICollection<CompanyCalendar>> GetAllAsync(CancellationToken cancellationToken)
    {
        var companyCalendars = await context.CompanyCalendars.ToListAsync(cancellationToken);

        return companyCalendars;
    }

    public async Task<CompanyCalendar> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var companyCalendar = await context
                                  .CompanyCalendars
                                  .FirstOrDefaultAsync(cc => cc.Id == id, cancellationToken) ??
                              throw new NotFoundException(nameof(CompanyCalendar), nameof(CompanyCalendar.Id));

        return companyCalendar;
    }
}
