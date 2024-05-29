﻿using System.Reflection;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Repositories;
using MediatR;

namespace EmployeeControl.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse>(ICurrentUserService currentUserService, IUserRepository userRepository)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
        var attributes = authorizeAttributes as AuthorizeAttribute[] ?? authorizeAttributes.ToArray();

        if (attributes.Length == 0)
        {
            return await next();
        }

        // Must be authenticated user.
        if (currentUserService.Id == default)
        {
            throw new UnauthorizedAccessException();
        }

        // Role-based authorization.
        await RoleBasedAuthorization(attributes, currentUserService.Id);

        // Policy-based authorization.
        await PolicyBasedAuthorization(attributes, currentUserService.Id);

        // User is authorized / authorization not required.
        return await next();
    }

    private async Task RoleBasedAuthorization(IEnumerable<AuthorizeAttribute> attributes, Guid userId)
    {
        var authorizeAttributesWithRoles = attributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));
        var attributesWithRoles = authorizeAttributesWithRoles as AuthorizeAttribute[] ?? authorizeAttributesWithRoles.ToArray();

        if (attributesWithRoles.Length != 0)
        {
            var authorized = false;

            foreach (var roles in attributesWithRoles.Select(a => a.Roles.Split(',')))
            {
                foreach (var role in roles)
                {
                    var isInRole = await userRepository.IsInRoleAsync(userId, role.Trim());

                    if (!isInRole)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                }
            }

            // Must be a member of at least one role in roles.
            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }
    }

    private async Task PolicyBasedAuthorization(IEnumerable<AuthorizeAttribute> attributes, Guid userId)
    {
        var authorizeAttributesWithPolicies = attributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));

        var attributesWithPolicies =
            authorizeAttributesWithPolicies as AuthorizeAttribute[] ?? authorizeAttributesWithPolicies.ToArray();

        if (attributesWithPolicies.Length == 0)
        {
            return;
        }

        foreach (var policy in attributesWithPolicies.Select(a => a.Policy))
        {
            var authorized = await userRepository.AuthorizeAsync(userId, policy);

            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }
    }
}
