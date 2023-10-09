using AutoMapper;
using EmployeeControl.Application.Common.Mapping;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.Register;

public class RegisterCommand : IRequest<string>, IMapFrom<ApplicationUser>
{
    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? ConfirmPassword { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterCommand, ApplicationUser>();
    }
}
