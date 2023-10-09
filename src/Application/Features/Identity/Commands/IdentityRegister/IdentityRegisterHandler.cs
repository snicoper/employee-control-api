using AutoMapper;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

internal class IdentityRegisterHandler
    (IIdentityService identityService, IMapper mapper) : IRequestHandler<IdentityRegisterCommand, string>
{
    public async Task<string> Handle(IdentityRegisterCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<IdentityRegisterCommand, ApplicationUser>(request);
        var password = request.Password.NotNull();

        var result = await identityService.CreateUserAsync(user, password);

        return result.Id;
    }
}
