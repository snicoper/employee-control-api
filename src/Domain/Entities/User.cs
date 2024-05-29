using System.ComponentModel.DataAnnotations.Schema;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Empleados de la aplicación.
/// </summary>
public class User : IdentityUser<Guid>, IEntityDomainEvent
{
    private readonly List<BaseEvent> _domainEvents = new();

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? RefreshToken { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public DateTimeOffset EntryDate { get; set; }

    public EmployeeSettings EmployeeSettings { get; set; } = null!;

    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public Guid CompanyCalendarId { get; set; }

    public CompanyCalendar CompanyCalendar { get; set; } = null!;

    public ICollection<EmployeeHoliday> EmployeeHolidays { get; set; } = [];

    public ICollection<EmployeeHolidayClaim> EmployeeHolidayClaims { get; set; } = [];

    public ICollection<EmployeeHolidayClaimItem> EmployeeHolidayClaimItems { get; set; } = [];

    public ICollection<TimeControl> TimeControls { get; set; } = [];

    public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = [];

    public ICollection<EmployeeCompanyTask> EmployeeCompanyTasks { get; set; } = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
