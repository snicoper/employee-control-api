using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Data.Seeds;

public class ApplicationDbContextInitialize(
    ILogger<ApplicationDbContextInitialize> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        await CreateCompanies();
        await CreateRoles();
        await CreateUsers();
    }

    private async Task CreateCompanies()
    {
        if (context.Company.Any(c => c.Name == "Company test"))
        {
            return;
        }

        var company = new Company { Name = "Company test" };

        await context.Company.AddAsync(company);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    private async Task CreateRoles()
    {
        // Default roles.
        var createRole = new List<IdentityRole>
        {
            new(Roles.Administrator), new(Roles.EnterpriseAdministrator), new(Roles.HumanResources), new(Roles.Employee)
        };

        foreach (var identityRole in createRole.Where(identityRole => roleManager.Roles.All(r => r.Name != identityRole.Name)))
        {
            await roleManager.CreateAsync(identityRole);
        }
    }

    private async Task CreateUsers()
    {
        var company = context.Company
            .AsNoTracking()
            .SingleAsync(c => c.Name == "Company test");

        // Default users.
        var administrator = new ApplicationUser
        {
            CompanyId = company.Id,
            UserName = "admin",
            FirstName = "Admin",
            LastName = "Admin1",
            Email = "admin@localhost"
        };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Password4!");
            await userManager.AddToRolesAsync(
                administrator,
                new[] { Roles.Administrator, Roles.Employee, Roles.HumanResources, Roles.EnterpriseAdministrator });
        }
    }
}
