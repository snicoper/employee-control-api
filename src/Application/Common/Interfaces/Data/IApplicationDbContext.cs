using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EmployeeControl.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }

    DbSet<CompanyTask> CompanyTasks { get; }

    DbSet<UserCompanyTask> UserCompanyTasks { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
