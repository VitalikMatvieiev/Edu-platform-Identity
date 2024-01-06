using Microsoft.AspNetCore.Authorization;

namespace Identity_Infrastructure.Authentication;

public class PermissionAuthorizationHandler 
            : AuthorizationHandler<PermissionRequirement>
{
    private readonly string permissionType = "claims";

    protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
    {
        var permissions = context
            .User
            .Claims
            .Where(x => x.Type == permissionType)
            .Select(x => x.Value);

        if (permissions.Contains(requirement.Permission))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}