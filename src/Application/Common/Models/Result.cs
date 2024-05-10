using EmployeeControl.Application.Common.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace EmployeeControl.Application.Common.Models;

public class Result
{
    protected Result(bool succeeded, ICollection<ValidationFailure> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
        Status = StatusCodes.Status200OK;
    }

    public bool Succeeded { get; private set; }

    public int Status { get; set; }

    public ICollection<ValidationFailure> Errors { get; }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result" /> con los valores predeterminados.
    /// </summary>
    /// <returns>Instancia de <see cref="Result" />.</returns>
    public static Result Create()
    {
        return new Result(true, []);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result{TValue}" /> con los valores predeterminados.
    /// </summary>
    /// <param name="value">Valor de tipo de resultado.</param>
    /// <typeparam name="TValue">Tipo esperado en el Result.</typeparam>
    /// <returns>Instancia de <see cref="Result{TValue}" />.</returns>
    public static Result<TValue> Create<TValue>(TValue? value)
    {
        return new Result<TValue>(value, true, []);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result" /> sin errores.
    /// </summary>
    /// <returns>Instancia de <see cref="Result" />.</returns>
    public static Result Success()
    {
        return new Result(true, []);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result{TValue}" /> sin errores.
    /// </summary>
    /// <param name="value">Valor de tipo de resultado.</param>
    /// <typeparam name="TValue">Tipo esperado en el Result.</typeparam>
    /// <returns>Instancia de <see cref="Result{TValue}" />.</returns>
    public static Result<TValue> Success<TValue>(TValue? value)
    {
        return new Result<TValue>(value, true, []);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result" /> con una colección de <see cref="ValidationFailure" />.
    /// </summary>
    /// <param name="errors">Lista de <see cref="ValidationFailure" />.</param>
    /// <returns>El <see cref="Result" /> con el error añadido.</returns>
    public static Result Failure(ICollection<ValidationFailure> errors)
    {
        return new Result(false, errors);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result{TValue}" /> con una colección de <see cref="ValidationFailure" />.
    /// </summary>
    /// <param name="errors">Lista de <see cref="ValidationFailure" />.</param>
    /// <typeparam name="TValue">Tipo esperado en el Result.</typeparam>
    /// <returns>El <see cref="Result{TValue}" /> con el error añadido.</returns>
    public static Result<TValue> Failure<TValue>(ICollection<ValidationFailure> errors)
    {
        return new Result<TValue>(default, false, errors);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result" /> con un error <see cref="ValidationFailure" />.
    /// </summary>
    /// <param name="validationFailure">Error a insertar.</param>
    /// <returns>El <see cref="Result" /> con el error añadido.</returns>
    public static Result Failure(ValidationFailure validationFailure)
    {
        return Failure([validationFailure]);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result{TValue}" /> con un error <see cref="ValidationFailure" />.
    /// </summary>
    /// <param name="validationFailure">Error a insertar.</param>
    /// <typeparam name="TValue">Tipo esperado en el Result.</typeparam>
    /// <returns>El <see cref="Result{TValue}" /> con el error añadido.</returns>
    public static Result<TValue> Failure<TValue>(ValidationFailure validationFailure)
    {
        return Failure<TValue>([validationFailure]);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result" /> con un error.
    /// </summary>
    /// <param name="code">Código del error.</param>
    /// <param name="error">Error a insertar.</param>
    /// <returns>El <see cref="Result" /> con el error añadido.</returns>
    public static Result Failure(string code, string error)
    {
        var validationFailure = new ValidationFailure(code, error);

        return Failure(validationFailure);
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Result{TValue}" /> con un error.
    /// </summary>
    /// <param name="code">Código del error.</param>
    /// <param name="error">Error a insertar.</param>
    /// <typeparam name="TValue">Tipo esperado en el Result.</typeparam>
    /// <returns>El <see cref="Result{TValue}" /> con el error añadido.</returns>
    public static Result<TValue> Failure<TValue>(string code, string error)
    {
        var validationFailure = new ValidationFailure(code, error);

        return Failure<TValue>(validationFailure);
    }

    /// <summary>
    /// Añade un nuevo error.
    /// </summary>
    /// <param name="validationFailure"><see cref="ValidationFailure" />.</param>
    public void AddValidationFailure(ValidationFailure validationFailure)
    {
        Succeeded = false;
        Status = StatusCodes.Status400BadRequest;

        Errors.Add(validationFailure);
    }

    /// <summary>
    /// Añade un nuevo error.
    /// </summary>
    /// <param name="code">Código del error.</param>
    /// <param name="error">Error a insertar.</param>
    public void AddError(string code, string error)
    {
        AddValidationFailure(new ValidationFailure(code, error));
    }

    /// <summary>
    /// Lanza un <see cref="CustomValidationException" /> si existen errores.
    /// </summary>
    /// <exception cref="CustomValidationException"><see cref="CustomValidationException" />.</exception>
    public void RaiseBadRequestIfResultFailure()
    {
        if (Succeeded)
        {
            return;
        }

        RaiseBadRequest();
    }

    /// <summary>
    /// Lanza un <see cref="CustomValidationException" /> con los errores producidos.
    /// </summary>
    /// <exception cref="CustomValidationException"><see cref="CustomValidationException" />.</exception>
    public void RaiseBadRequest()
    {
        throw new CustomValidationException([.. Errors]);
    }

    /// <summary>
    /// Obtener el <see cref="Result" /> en caso de no haber errores,
    /// en caso contrario, lanzara un <see cref="CustomValidationException" /> con los errores.
    /// </summary>
    /// <returns>Instancia de <see cref="Result" /> en caso de no tener errores.</returns>
    public Result RaiseOrGetSuccess()
    {
        if (!Succeeded)
        {
            RaiseBadRequest();
        }

        return this;
    }
}

#pragma warning disable SA1402 // File may only contain a single type
public class Result<TValue> : Result
{
    protected internal Result(TValue? value, bool succeeded, ICollection<ValidationFailure> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; set; }

    /// <summary>
    /// Obtener el <see cref="Result{TValue}" /> en caso de no haber errores,
    /// en caso contrario, lanzara un <see cref="CustomValidationException" /> con los errores.
    /// </summary>
    /// <returns>Instancia de <see cref="Result{TValue}" /> en caso de no tener errores.</returns>
    public new Result<TValue> RaiseOrGetSuccess()
    {
        if (!Succeeded)
        {
            RaiseBadRequest();
        }

        return this;
    }
}
#pragma warning disable SA1402 // File may only contain a single type
