namespace EmployeeControl.Domain.Constants;

public abstract class Roles
{
    /// <summary>
    /// Administrador del sitio.
    /// </summary>
    public const string Administrator = nameof(Administrator);

    /// <summary>
    /// Staff del sitio.
    /// </summary>
    public const string Staff = nameof(Staff);

    /// <summary>
    /// Administrador de empresa.
    /// </summary>
    public const string EnterpriseAdministrator = nameof(EnterpriseAdministrator);

    /// <summary>
    /// Recursos humanos (RRHH) de empresa.
    /// </summary>
    public const string HumanResources = nameof(HumanResources);

    /// <summary>
    /// Empleado de empresa.
    /// </summary>
    public const string Employee = nameof(Employee);
}
