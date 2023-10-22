namespace EmployeeControl.Application.Common.Constants;

public static class UrlsWebApp
{
    /// <summary>
    /// Url para la validación de Email del usuario.
    /// </summary>
    public const string EmailRegisterValidate = "identity/register-validate";

    /// <summary>
    /// Url validación de una invitación por parte de una empresa.
    /// </summary>
    public const string InviteEmployee = "employee/invite-employee-validate";

    /// <summary>
    /// Url del formulario para restablecer contraseña olvidada.
    /// </summary>
    public const string RecoveryPasswordChange = "identity/recovery-password-change";
}
