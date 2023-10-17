using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Entities.Identity;

public interface IIdentityValidatorService
{
    Task UniqueEmailValidationAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);

    Task UserValidationAsync(ApplicationUser applicationUser);

    Task PasswordValidationAsync(ApplicationUser applicationUser, string password);
}
