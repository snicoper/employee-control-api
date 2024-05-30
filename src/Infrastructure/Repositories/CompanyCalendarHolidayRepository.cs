using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class CompanyCalendarHolidayRepository(
    IApplicationDbContext context,
    ICompanyCalendarHolidaysValidator companyCalendarHolidaysValidator)
    : ICompanyCalendarHolidayRepository
{
    public async Task<CompanyCalendarHoliday> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var companyHoliday = await context
                .CompanyCalendarHoliday
                .FirstOrDefaultAsync(ch => ch.Id == id, cancellationToken)
            ?? throw new NotFoundException(nameof(CompanyCalendarHoliday), nameof(CompanyCalendarHoliday.Id));

        return companyHoliday;
    }

    public async Task<CompanyCalendarHoliday> CreateAsync(
        CompanyCalendarHoliday companyCalendarHoliday,
        CancellationToken cancellationToken)
    {
        await companyCalendarHolidaysValidator
            .ValidateCreateHolidayInDateAsync(companyCalendarHoliday, cancellationToken);

        context.CompanyCalendarHoliday.Add(companyCalendarHoliday);
        await context.SaveChangesAsync(cancellationToken);

        return companyCalendarHoliday;
    }

    public async Task<CompanyCalendarHoliday> UpdateAsync(
        CompanyCalendarHoliday companyCalendarHoliday,
        CancellationToken cancellationToken)
    {
        await companyCalendarHolidaysValidator
            .ValidateUpdateHolidayInDateAsync(companyCalendarHoliday, cancellationToken);

        context.CompanyCalendarHoliday.Update(companyCalendarHoliday);
        await context.SaveChangesAsync(cancellationToken);

        return companyCalendarHoliday;
    }

    public async Task DeleteAsync(CompanyCalendarHoliday companyCalendarHoliday, CancellationToken cancellationToken)
    {
        context.CompanyCalendarHoliday.Remove(companyCalendarHoliday);
        await context.SaveChangesAsync(cancellationToken);
    }
}
