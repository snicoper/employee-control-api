﻿using System.ComponentModel.DataAnnotations.Schema;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

public class ApplicationUser : IdentityUser, ICompany, IEntityDomainEvent
{
    private readonly List<BaseEvent> _domainEvents;

    public ApplicationUser()
    {
        _domainEvents = new List<BaseEvent>();
        UserCompanyTasks = new List<UserCompanyTask>();
        TimeControls = new List<TimeControl>();
        UserDepartments = new List<UserDepartment>();
    }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? RefreshToken { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public DateTimeOffset? EntryDate { get; set; }

    public UserSettings UserSettings { get; set; } = null!;

    public ICollection<UserCompanyTask> UserCompanyTasks { get; set; }

    public ICollection<TimeControl> TimeControls { get; set; }

    public ICollection<UserDepartment> UserDepartments { get; set; }

    public Company Company { get; set; } = null!;

    public string CompanyId { get; set; } = default!;

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
