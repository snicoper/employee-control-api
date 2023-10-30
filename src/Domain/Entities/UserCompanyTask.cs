using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class UserCompanyTask : ICompany
{
    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }

    public string? CompanyTaskId { get; set; }

    public CompanyTask? CompanyTask { get; set; }

    public Company? Company { get; set; }

    public string CompanyId { get; set; } = default!;
}
