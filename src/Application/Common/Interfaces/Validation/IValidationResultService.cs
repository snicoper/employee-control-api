using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Common.Interfaces.Validation;

public interface IValidationResultService
{
    /// <summary>
    /// Comprueba si tiene errores.
    /// </summary>
    /// <returns>true en caso de existir errores, false en caso contrario.</returns>
    bool HasErrors();

    /// <summary>
    /// Obtener el numero de errores.
    /// </summary>
    int ErrorsCount();

    /// <summary>
    /// Añadir un error.
    /// </summary>
    void Add(string property, string error);

    /// <summary>
    /// Añadir un diccionario con errores.
    /// </summary>
    void Add(Dictionary<string, string> errors);

    /// <summary>
    /// Añade un error y lanza la excepción con los errores.
    /// </summary>
    void AddAndRaiseException(string key, string value);

    /// <summary>
    /// Añade un diccionario con errores y lanza la excepción con los errores.
    /// </summary>
    void AddAndRaiseException(Dictionary<string, string> errors);

    /// <summary>
    /// Lanza la excepción tenga o no errores.
    /// </summary>
    void RaiseException();

    /// <summary>
    /// Lanza la excepción si tiene errores.
    /// </summary>
    void RaiseExceptionIfExistsErrors();

    /// <summary>
    /// Convierte un <see cref="IValidationResultService" /> a <see cref="Result" />.
    /// </summary>
    /// <returns><see cref="Result" />.</returns>
    Result ToResult();

    /// <summary>
    /// Convierte un <see cref="IValidationResultService" /> a <see cref="Result{TValue}" />.
    /// </summary>
    /// <typeparam name="TValue">Valor a insertar a Result.</typeparam>
    /// <returns><see cref="Result{TValue}" />.</returns>
    Result<TValue> ToResult<TValue>(TValue value);
}
