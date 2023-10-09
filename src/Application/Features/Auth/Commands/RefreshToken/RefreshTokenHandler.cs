using EmployeeControl.Application.Common.Interfaces.Identity;
using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

internal class RefreshTokenHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
{
    public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenAsync(request.RefreshToken);

        return new RefreshTokenDto { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken };
    }
}
