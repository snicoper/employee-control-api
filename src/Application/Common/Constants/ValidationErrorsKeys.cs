namespace EmployeeControl.Application.Common.Constants;

public static class ValidationErrorsKeys
{
    /// <summary>
    /// Errores relacionados con formularios que no son de un campo especifico.
    /// </summary>
    public const string NonFieldErrors = nameof(NonFieldErrors);

    /// <summary>
    /// Errores de notificación.
    /// </summary>
    public const string NotificationErrors = nameof(NotificationErrors);

    /// <summary>
    /// Errores de Identity.
    /// </summary>
    public const string IdentityError = nameof(IdentityError);

    /// <summary>
    /// Errores de TimeControl.
    /// </summary>
    public const string TimeControlError = nameof(TimeControlError);
}
