using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Data.Seeds;

public class ApplicationDbContextInitialize
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialize> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextInitialize(
        ILogger<ApplicationDbContextInitialize> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
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
            _logger.LogError(ex, "An error occurred while seeding the database.");
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
        if (_context.Company.Any(c => c.Name == "Company test"))
        {
            return;
        }

        var company = new Company { Name = "Company test" };

        await _context.Company.AddAsync(company);
        await _context.SaveChangesAsync(CancellationToken.None);
    }

    private async Task CreateRoles()
    {
        // Default roles.
        var createRole = new List<IdentityRole>
        {
            new(Roles.Administrator), new(Roles.EnterpriseAdministrator), new(Roles.HumanResources), new(Roles.Employee)
        };

        foreach (var identityRole in createRole.Where(identityRole => _roleManager.Roles.All(r => r.Name != identityRole.Name)))
        {
            await _roleManager.CreateAsync(identityRole);
        }
    }

    private async Task CreateUsers()
    {
        var company = _context.Company
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

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Password4!");
            await _userManager.AddToRolesAsync(
                administrator,
                new[] { Roles.Administrator, Roles.Employee, Roles.HumanResources, Roles.EnterpriseAdministrator });
        }
    }
}
