using AutoMapper;
using EmployeeControl.Application.Common.Mapping;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<string>, IMapFrom<ApplicationUser>
{
    public RegisterCommand(string? userName, string? email, string? password, string? confirmPassword, string companyName)
    {
        UserName = userName;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
        CompanyName = companyName;
    }

    public string? UserName { get; }

    public string? Email { get; }

    public string? Password { get; }

    public string? ConfirmPassword { get; }

    public string? CompanyName { get; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterCommand, ApplicationUser>();
    }
}
