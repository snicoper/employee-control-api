using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

internal sealed class LoginHandler(IAuthService authService) : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request.Email, request.Password);
        var resultResponse = new LoginResponse(result.AccessToken, result.RefreshToken);

        return Result.Success(resultResponse);
    }
}
