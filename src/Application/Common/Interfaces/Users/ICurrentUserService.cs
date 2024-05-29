namespace EmployeeControl.Application.Common.Interfaces.Users;

public interface ICurrentUserService
{
    Guid Id { get; }

    Guid CompanyId { get; }

    IEnumerable<string> Roles { get; }
}
