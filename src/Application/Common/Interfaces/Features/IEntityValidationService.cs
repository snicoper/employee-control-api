using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Application.Common.Interfaces.Features;

public interface IEntityValidationService
{
    /// <summary>
    /// Comprueba si CompanyId es igual a <see cref="ICurrentUserService" />.
    /// <para>
    /// Si el <see cref="Roles" /> es <see cref="Roles.Staff" /> o <see cref="Roles.Administrator" />,
    /// se considera que si tiene permisos. De lo contrario, solo podrá leer los datos en caso de que el
    /// <see cref="ICurrentUserService.CompanyId" /> sea igual a TEntity.CompanyId.
    /// </para>
    /// </summary>
    /// <param name="entity">Entidad a comprobar.</param>
    /// <typeparam name="TEntity">Entidad.</typeparam>
    /// <exception cref="NotFoundException">Si no tiene permisos para leer los datos.</exception>
    Task CheckEntityCompanyIsOwner<TEntity>(TEntity entity)
        where TEntity : ICompanyId;
}
