using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Company> Company { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
