using EmployeeControl.Application.Common.Interfaces.Identity;
using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

internal class LoginHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginDto>
{
    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request.Identifier, request.Password);
        var resultResponse = new LoginDto { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };

        return resultResponse;
    }
}
