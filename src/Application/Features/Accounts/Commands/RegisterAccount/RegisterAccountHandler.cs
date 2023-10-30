using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Company;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Accounts.Commands.RegisterAccount;

internal class RegisterAccountHandler(
        UserManager<ApplicationUser> userManager,
        IIdentityService identityService,
        ICompanyService companyService,
        IApplicationDbContext context,
        IIdentityEmailsService identityEmailsService,
        ILogger<RegisterAccountHandler> logger)
    : IRequestHandler<RegisterAccountCommand, string>
{
    public async Task<string> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Crear compañía en una transaction.
            var newCompany = new Company { Name = request.CompanyName };
            var company = await companyService.CreateCompanyAsync(newCompany, cancellationToken);

            var user = new ApplicationUser
            {
                FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, CompanyId = company.Id
            };

            var password = request.Password;

            // Roles para usuario y creación del usuario.
            var roles = new List<string> { new(Roles.EnterpriseAdministrator), new(Roles.HumanResources), new(Roles.Employee) };
            var (_, userId) = await identityService.CreateAccountAsync(user, password, roles, cancellationToken);

            // Generar code de validación.
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            // Mandar email de verificación de Email.
            await identityEmailsService.SendValidateEmailAsync(user, company, code);

            await transaction.CommitAsync(cancellationToken);

            return userId;
        }
        catch (Exception ex)
        {
            logger.LogDebug("{message}", ex.Message);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
