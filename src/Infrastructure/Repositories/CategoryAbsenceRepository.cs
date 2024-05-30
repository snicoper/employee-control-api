using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Repositories;

public class CategoryAbsenceRepository(IApplicationDbContext context, IStringLocalizer<CategoryAbsenceResource> localizer)
    : ICategoryAbsenceRepository
{
    public async Task<CategoryAbsence> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var categoryAbsence = await context
            .CategoryAbsences
            .SingleOrDefaultAsync(ca => ca.Id == id, cancellationToken)
                ?? throw new NotFoundException(nameof(CategoryAbsence), nameof(CategoryAbsence.Id));

        return categoryAbsence;
    }

    public async Task<CategoryAbsence> CreateAsync(CategoryAbsence categoryAbsence, CancellationToken cancellationToken)
    {
        var categoryExists = await context
            .CategoryAbsences
            .AnyAsync(
                ca => ca.Description.ToLower() == categoryAbsence.Description.ToLower(),
                cancellationToken);

        if (categoryExists)
        {
            var errorMessage = localizer["La descripción ya existe en la base de datos."];

            Result
                .Failure(nameof(CategoryAbsence.Description), errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }

        categoryAbsence.Active = true;

        context.CategoryAbsences.Add(categoryAbsence);
        await context.SaveChangesAsync(cancellationToken);

        return categoryAbsence;
    }

    public async Task<CategoryAbsence> UpdateAsync(CategoryAbsence categoryAbsence, CancellationToken cancellationToken)
    {
        var categoryAbsenceExist = await context
            .CategoryAbsences
            .AnyAsync(
                ca => ca.Description.ToLower() == categoryAbsence.Description.ToLower() && ca.Id != categoryAbsence.Id,
                cancellationToken);

        if (categoryAbsenceExist)
        {
            var errorMessage = localizer["La descripción ya existe en la base de datos."];
            Result
                .Failure(nameof(CategoryAbsence.Description), errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }

        context.CategoryAbsences.Update(categoryAbsence);
        await context.SaveChangesAsync(cancellationToken);

        return categoryAbsence;
    }
}
