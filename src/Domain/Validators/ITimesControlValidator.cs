using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Domain.Validators;

public interface ITimesControlValidator
{
    /// <summary>
    /// Reglas de validación de un <see cref="TimeControl" /> en la creación.
    /// <para>Un tiempo creado no deberá estar en algún rango de tiempo ya creado.</para>
    /// <para>Un tiempo creado no deberá estar en algún rango de tiempo que actualmente este abierto.</para>
    /// </summary>
    /// <param name="timeControl"><see cref="TimeControl" />.</param>
    /// <param name="result"><see cref="Result" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task<Result> ValidateCreateAsync(TimeControl timeControl, Result result, CancellationToken cancellationToken);

    /// <summary>
    /// Reglas de validación de un <see cref="TimeControl" /> en la actualización.
    /// <para>Un tiempo actualizado no deberá estar en algún rango de tiempo ya creado.</para>
    /// <para>Un tiempo actualizado no deberá estar en algún rango de tiempo que actualmente este abierto.</para>
    /// <para>Las reglas excluyen al tiempo actual.</para>
    /// </summary>
    /// <param name="timeControl"><see cref="TimeControl" />.</param>
    /// <param name="result"><see cref="Result" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task<Result> ValidateUpdateAsync(TimeControl timeControl, Result result, CancellationToken cancellationToken);
}
