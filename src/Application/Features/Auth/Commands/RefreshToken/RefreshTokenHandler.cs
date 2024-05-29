using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Domain.Common;

namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

internal sealed class RefreshTokenHandler(IAuthService authService) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenAsync(request.RefreshToken);
        var resultResponse = new RefreshTokenResponse(result.AccessToken, result.RefreshToken);

        return Result.Success(resultResponse);
    }
}
