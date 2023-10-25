using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Company : BaseAuditableEntity
{
    public Company()
    {
    }

    public Company(string? name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
}
