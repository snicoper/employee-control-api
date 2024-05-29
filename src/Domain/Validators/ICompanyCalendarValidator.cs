using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Domain.Validators;

public interface ICompanyCalendarValidator
{
    /// <summary>
    /// Validaciones para crear un <see cref="CompanyCalendar" />.
    /// </summary>
    /// <param name="companyCalendar"><see cref="CompanyCalendar" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task CreateValidationAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken);

    /// <summary>
    /// Validaciones para actualizar un <see cref="CompanyCalendar" />.
    /// </summary>
    /// <param name="companyCalendar"><see cref="CompanyCalendar" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task UpdateValidationAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken);
}
