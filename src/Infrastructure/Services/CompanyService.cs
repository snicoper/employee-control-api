using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services;

public class CompanyService(ApplicationDbContext context) : ICompanyService
{
    public async Task CreateCompany(string name, CancellationToken cancellationToken)
    {
        var company = await context.Company.SingleOrDefaultAsync(c => c.Name == name, cancellationToken);
        throw new NotImplementedException();
    }
}
