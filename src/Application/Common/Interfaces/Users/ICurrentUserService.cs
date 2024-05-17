namespace EmployeeControl.Application.Common.Interfaces.Users;

public interface ICurrentUserService
{
    string Id { get; }

    string CompanyId { get; }

    IEnumerable<string> Roles { get; }
}
