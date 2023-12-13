using Identity_Application.Interfaces;
using Identity_Application.Interfaces.Authentication;
using Identity_Domain.Entities.Base;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace Identity_Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private static readonly double tokenHourExpTime = 6;
    private static readonly string securitykey = "security-key-phrase";
    private static readonly string tokenIssuer = "edu_learn_identity";

    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Identity identity)
    {
        var claimsJson = GetClaimsJson(identity.Roles, identity.Claims);
        var rolesJson = GetRolesJson(identity.Roles);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey)),
                SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new System.Security.Claims.Claim("id", identity.Id.ToString()),
            new System.Security.Claims.Claim("username", identity.Username),
            new System.Security.Claims.Claim("email", identity.Email),
            new System.Security.Claims.Claim("lastLogin", identity.LastLogin.Millisecond.ToString()),
            new System.Security.Claims.Claim("roles", rolesJson),
            new System.Security.Claims.Claim("claims", claimsJson)
        };

        var securityToken = new JwtSecurityToken(
            issuer: tokenIssuer,
            expires: _dateTimeProvider.UtcNow.AddHours(tokenHourExpTime),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private string GetRolesJson(List<Role> roles)
    {
        var rolesToJson = new List<string>();

        foreach (var role in roles)
        {
            rolesToJson.Add(role.Name);
        }

        return JsonSerializer.Serialize(rolesToJson);
    }

    private string GetClaimsJson(List<Role> roles, List<Claim> claims)
    {
        var claimsToJson = new List<string>();

        foreach (var claim in claims)
        {
            claimsToJson.Add(claim.Name);
        }

        foreach (var role in roles)
        {
            foreach (var claim in role.Claims)
            {
                if (!claimsToJson.Contains(claim.Name))
                    claimsToJson.Add(claim.Name);
            }
        }

        return JsonSerializer.Serialize(claimsToJson);
    }
}