﻿using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class CompanyCalendarRepository(
    IApplicationDbContext context,
    ICompanyCalendarValidatorService companyCalendarValidatorService)
    : ICompanyCalendarRepository
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

    public async Task<CompanyCalendar> CreateAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken)
    {
        await companyCalendarValidatorService.CreateValidationAsync(companyCalendar, cancellationToken);

        context.CompanyCalendars.Add(companyCalendar);
        await context.SaveChangesAsync(cancellationToken);

        if (companyCalendar.Default)
        {
            await SetDefaultCalendarAsync(companyCalendar, cancellationToken);
        }

        return companyCalendar;
    }

    public async Task<CompanyCalendar> UpdateAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken)
    {
        await companyCalendarValidatorService.UpdateValidationAsync(companyCalendar, cancellationToken);

        context.CompanyCalendars.Update(companyCalendar);
        await context.SaveChangesAsync(cancellationToken);

        return companyCalendar;
    }

    public async Task SetDefaultCalendarAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken)
    {
        companyCalendar.Default = true;

        var currentDefault = await context
            .CompanyCalendars
            .Where(cc => cc.Default && cc.Id != companyCalendar.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (currentDefault is null)
        {
            context.CompanyCalendars.Update(companyCalendar);
        }
        else
        {
            currentDefault.Default = false;
            context.CompanyCalendars.UpdateRange(currentDefault, companyCalendar);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
