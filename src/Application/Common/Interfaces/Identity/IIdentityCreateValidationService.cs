using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface IIdentityCreateValidationService
{
    Task ValidateUniqueEmail(ApplicationUser applicationUser, CancellationToken cancellationToken);

    Task UserValidationAsync(ApplicationUser applicationUser);

    Task PasswordValidationAsync(ApplicationUser applicationUser, string password);
}
