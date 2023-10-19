namespace EmployeeControl.Application.Common.Constants;

public abstract class UrlsWebApp
{
    /// <summary>
    /// Url para la validación de Email del usuario.
    /// </summary>
    public const string EmailRegisterValidate = "identity/register-validate";

    /// <summary>
    /// Url del formulario para restablecer contraseña olvidada.
    /// </summary>
    public const string RecoveryPasswordChange = "identity/recovery-password-change";
}
