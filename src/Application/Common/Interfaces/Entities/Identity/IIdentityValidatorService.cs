using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Entities.Identity;

public interface IIdentityValidatorService
{
    Task UniqueEmailValidationAsync(ApplicationUser user, CancellationToken cancellationToken);

    Task UserValidationAsync(ApplicationUser user);

    Task PasswordValidationAsync(ApplicationUser user, string password);
}
