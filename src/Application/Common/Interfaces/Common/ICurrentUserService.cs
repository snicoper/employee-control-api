namespace EmployeeControl.Application.Common.Interfaces.Common;

public interface ICurrentUserService
{
    string? Id { get; }

    int CompanyId { get; }
}
