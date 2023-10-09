using AutoMapper;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.Register;

internal class RegisterHandler(IIdentityService identityService, IMapper mapper) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<RegisterCommand, ApplicationUser>(request);
        var password = request.Password.NotNull();

        var result = await identityService.CreateUserAsync(user, password);

        return result.Id;
    }
}
