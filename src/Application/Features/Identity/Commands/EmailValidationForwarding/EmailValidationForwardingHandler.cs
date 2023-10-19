using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Identity.Commands.EmailValidationForwarding;

internal class EmailValidationForwardingHandler(
        UserManager<ApplicationUser> userManager,
        IIdentityEmailsService identityEmailsService,
        IStringLocalizer<IdentityLocalizer> localizer)
    : IRequestHandler<EmailValidationForwardingCommand, Result>
{
    public async Task<Result> Handle(EmailValidationForwardingCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .Include(au => au.Company)
            .SingleOrDefaultAsync(au => au.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            var message = localizer["El usuario no existe en la base de datos."];

            return Result.Failure(message);
        }

        if (user.EmailConfirmed)
        {
            var message = localizer["El usuario ya ha confirmado el correo electrónico."];

            return Result.Failure(message);
        }

        if (user.Company is null)
        {
            var message = localizer["El usuario debe pertenecer a una empresa."];

            return Result.Failure(message);
        }

        await identityEmailsService.SendValidateEmailAsync(user, user.Company);

        return Result.Success();
    }
}
