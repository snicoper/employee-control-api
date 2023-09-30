using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localization;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Common.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IStringLocalizer<IdentityLocalizer> _localizer;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidationFailureService _validationFailureService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IValidationFailureService validationFailureService,
        IStringLocalizer<IdentityLocalizer> localizer)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _validationFailureService = validationFailureService;
        _localizer = localizer;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result, string Id)> CreateUserAsync(ApplicationUser applicationUser, string password)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == applicationUser.UserName);

        if (user != null)
        {
            // Comprobar si el userName es único.
            _validationFailureService.AddAndRaiseException(
                nameof(ApplicationUser.UserName),
                _localizer["Nombre de usuario ya registrado"]);

            // Comprobar si el email es único.
            _validationFailureService.AddAndRaiseException(
                nameof(ApplicationUser.Email),
                _localizer["Email de usuario ya registrado"]);
        }

        var result = await _userManager.CreateAsync(applicationUser, password);
        await _userManager.AddToRolesAsync(applicationUser, new[] { Roles.Employee });

        return (result.ToApplicationResult(), applicationUser.Id);
    }
}
