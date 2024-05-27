using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Emails;

public interface IDepartmentEmailsService
{
    /// <summary>
    /// Envía un email a los empleados que se les a añadido a un departamento.
    /// </summary>
    /// <param name="department"><see cref="Department" />.</param>
    /// <param name="company"><see cref="Company" />.</param>
    /// <param name="users">Lista de <see cref="User" /> añadidos al departamento.</param>
    Task SendEmployeeAssignDepartmentAsync(Department department, Company company, List<User> users);
}
