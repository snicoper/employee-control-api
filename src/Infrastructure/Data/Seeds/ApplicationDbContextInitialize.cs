using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Data.Seeds;

public class ApplicationDbContextInitialize(
    ILogger<ApplicationDbContextInitialize> logger,
    ApplicationDbContext context,
    TimeProvider timeProvider,
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

    private async Task TrySeedAsync()
    {
        await CreateCompanies();
        await CreateRoles();
        await CreateUsers();
    }

    private async Task CreateCompanies()
    {
        var companies = new List<Company> { new() { Name = "Company test" }, new() { Name = "Test Company" } };

        foreach (var company in companies.Where(company => !context.Companies.Any(c => c.Name == company.Name)))
        {
            await context.Companies.AddAsync(company);
        }

        await context.SaveChangesAsync(CancellationToken.None);
    }

    private async Task CreateRoles()
    {
        // Default roles.
        var createRole = new List<IdentityRole>
        {
            new(Roles.Administrator),
            new(Roles.Staff),
            new(Roles.EnterpriseAdministrator),
            new(Roles.HumanResources),
            new(Roles.Employee)
        };

        foreach (var identityRole in createRole.Where(identityRole => roleManager.Roles.All(r => r.Name != identityRole.Name)))
        {
            await roleManager.CreateAsync(identityRole);
        }
    }

    private async Task CreateUsers()
    {
        var companies = context.Companies.ToList();

        // Administrator user.
        var user = new ApplicationUser
        {
            CompanyId = companies[0].Id,
            UserName = "admin@localhost",
            FirstName = "Admin",
            LastName = "Admin1",
            Email = "admin@localhost",
            EntryDate = timeProvider.GetUtcNow(),
            Active = true,
            EmailConfirmed = true
        };

        if (!await userManager.Users.AnyAsync(u => u.Email == user.Email))
        {
            await userManager.CreateAsync(user, "Password4!");

            var rolesToAdd = new[]
            {
                Roles.Administrator, Roles.Staff, Roles.EnterpriseAdministrator, Roles.HumanResources, Roles.Employee
            };

            await userManager.AddToRolesAsync(user, rolesToAdd);
        }

        // EnterpriseAdministrator user.
        user = new ApplicationUser
        {
            CompanyId = companies[1].Id,
            UserName = "snicoper@gmail.com",
            FirstName = "Salvador",
            LastName = "Nicolas",
            Email = "snicoper@gmail.com",
            EntryDate = timeProvider.GetUtcNow(),
            Active = true,
            EmailConfirmed = true
        };

        if (!await userManager.Users.AnyAsync(u => u.Email == user.Email))
        {
            await userManager.CreateAsync(user, "Password4!");

            var rolesToAdd = new[] { Roles.EnterpriseAdministrator, Roles.HumanResources, Roles.Employee };

            await userManager.AddToRolesAsync(user, rolesToAdd);
        }
    }
}
