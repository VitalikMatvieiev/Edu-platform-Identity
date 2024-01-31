using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Identity_Infrastructure.Authentication;

public class PermissionAuthorizationPolicyProvider
                : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options) : base(options)
    {

    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy policy;

        try
        {
            policy = await base.GetPolicyAsync(policyName);
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during policy fetching", ex);
        }

        if (policy is not null)
            return policy;

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}