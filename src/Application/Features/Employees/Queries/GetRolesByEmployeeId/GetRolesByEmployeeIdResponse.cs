using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

public record GetRolesByEmployeeIdResponse(List<IdentityRole> Roles);
