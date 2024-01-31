using Identity_Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Identity_Infrastructure.Authentication;

public sealed class HasClaimAttribute : AuthorizeAttribute
{
    public HasClaimAttribute(ControllersClaims claim)
        : base(policy: claim.ToString())
    {
        
    }
}