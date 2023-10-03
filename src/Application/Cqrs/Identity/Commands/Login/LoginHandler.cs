using EmployeeControl.Application.Common.Interfaces.Identity;
using MediatR;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly IAuthService _authService;

    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request.Identifier, request.Password);

        return new LoginDto { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };
    }
}
