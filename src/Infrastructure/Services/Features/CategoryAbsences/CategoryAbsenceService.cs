using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CategoryAbsences;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.CategoryAbsences;

public class CategoryAbsenceService(
    IApplicationDbContext context,
    IValidationFailureService validationFailureService,
    IStringLocalizer<CategoryAbsenceLocalizer> localizer)
    : ICategoryAbsenceService
{
    public async Task<CategoryAbsence> CreateAsync(CategoryAbsence categoryAbsence, CancellationToken cancellationToken)
    {
        var categoryExists = await context.CategoryAbsences.AnyAsync(
            ca => ca.Description == categoryAbsence.Description,
            cancellationToken);

        if (categoryExists)
        {
            var message = localizer["La descripción ya existe en la base de datos."];
            validationFailureService.AddAndRaiseException(nameof(CategoryAbsence.Description), message);
        }

        context.CategoryAbsences.Add(categoryAbsence);
        await context.SaveChangesAsync(cancellationToken);

        return categoryAbsence;
    }
}
