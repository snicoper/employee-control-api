using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class UserCompanyTask : ICompany
{
    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;

    public string CompanyTaskId { get; set; } = default!;

    public CompanyTask CompanyTask { get; set; } = null!;

    public Company Company { get; set; } = null!;

    public string CompanyId { get; set; } = default!;
}
