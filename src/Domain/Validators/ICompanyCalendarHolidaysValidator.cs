using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Domain.Validators;

public interface ICompanyCalendarHolidaysValidator
{
    /// <summary>
    /// Comprueba si la compañía ya tiene un día festivo.
    /// </summary>
    /// <param name="companyCalendarHoliday">Datos de <see cref="CompanyCalendarHoliday" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task ValidateCreateHolidayInDateAsync(CompanyCalendarHoliday companyCalendarHoliday, CancellationToken cancellationToken);

    /// <summary>
    /// Comprueba si la compañía ya tiene un día festivo.
    /// </summary>
    /// <param name="companyCalendarHoliday">Datos de <see cref="CompanyCalendarHoliday" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task ValidateUpdateHolidayInDateAsync(CompanyCalendarHoliday companyCalendarHoliday, CancellationToken cancellationToken);
}
