namespace EmployeeControl.Domain.Constants;

public abstract class Roles
{
    /// <summary>
    /// Administrador del sitio.
    /// </summary>
    public const string Administrator = nameof(Administrator);

    /// <summary>
    /// Administrador de empresa.
    /// </summary>
    public const string EnterpriseAdministrator = nameof(EnterpriseAdministrator);

    /// <summary>
    /// Recursos humanos (RRHH).
    /// </summary>
    public const string HumanResources = nameof(HumanResources);

    /// <summary>
    /// Empleado.
    /// </summary>
    public const string Employee = nameof(Employee);
}
