using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

internal class IdentityRegisterHandler(
        IIdentityService identityService,
        IApplicationDbContext context,
        ILogger<IdentityRegisterHandler> logger)
    : IRequestHandler<IdentityRegisterCommand, string>
{
    public async Task<string> Handle(IdentityRegisterCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Crear compañía en una transaction.
            var company = new Company { Name = request.CompanyName };
            await context.Company.AddAsync(company, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Active = true,
                CompanyId = company.Id
            };

            var password = request.Password.SetEmptyIfNull();

            // Roles para usuario creado.
            var roles = new List<string>
            {
                new(Roles.EnterpriseAdministrator), new(Roles.Staff), new(Roles.HumanResources), new(Roles.Employee)
            };

            var resultResponse = await identityService.CreateUserAsync(user, password, roles);

            await transaction.CommitAsync(cancellationToken);

            return resultResponse.Id;
        }
        catch (Exception ex)
        {
            logger.LogDebug("{message}", ex.Message);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
