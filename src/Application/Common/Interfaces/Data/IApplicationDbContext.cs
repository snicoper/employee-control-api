using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeeControl.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }

    DbSet<CompanySettings> CompanySettings { get; }

    DbSet<CompanyTask> CompanyTasks { get; }

    DbSet<Department> Departments { get; }

    DbSet<TimeControl> TimeControls { get; }

    DbSet<EmployeeCompanyTask> UserCompanyTasks { get; }

    DbSet<EmployeeDepartment> UserDepartments { get; }

    DbSet<EmployeeSettings> UserSettings { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
