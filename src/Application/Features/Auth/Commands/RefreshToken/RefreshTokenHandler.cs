using EmployeeControl.Application.Common.Interfaces.Identity;
using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
{
    private readonly IAuthService _authService;

    public RefreshTokenHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken);

        return new RefreshTokenDto { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };
    }
}
