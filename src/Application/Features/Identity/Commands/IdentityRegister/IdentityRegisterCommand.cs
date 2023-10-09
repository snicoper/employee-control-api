using AutoMapper;
using EmployeeControl.Application.Common.Mapping;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

public record IdentityRegisterCommand(
        string? UserName,
        string? Email,
        string? Password,
        string? ConfirmPassword,
        string CompanyName)
    : IRequest<string>, IMapFrom<ApplicationUser>
{
    public string? UserName { get; } = UserName;

    public string? Email { get; } = Email;

    public string? Password { get; } = Password;

    public string? ConfirmPassword { get; } = ConfirmPassword;

    public string? CompanyName { get; } = CompanyName;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IdentityRegisterCommand, ApplicationUser>();
    }
}
