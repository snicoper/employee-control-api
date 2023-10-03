using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
{
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService,
        IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtSettings = options.Value;
    }

    public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.RefreshToken == request.RefreshToken);

        if (user is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new ForbiddenAccessException();
        }

        var jwt = await _tokenService.GenerateAccessTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationTokenLifeTimeDays);

        await _userManager.UpdateAsync(user);

        return new RefreshTokenDto { AccessToken = jwt, RefreshToken = refreshToken };
    }
}
