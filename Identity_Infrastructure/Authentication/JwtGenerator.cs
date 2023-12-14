using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AppSettingsModels;
using Identity_Domain.Entities.Base;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace Identity_Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private readonly double tokenHourExpTime;
    private readonly string securitykey;
    private readonly string tokenIssuer;

    private readonly IOptions<JwtSettings> _config;

    public JwtGenerator(IOptions<JwtSettings> config)
    {
        _config = config;

        tokenHourExpTime = _config.Value.TokenHourExpTime;
        securitykey = _config.Value.Securitykey;
        tokenIssuer = _config.Value.TokenIssuer;
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
            expires: DateTime.UtcNow.AddHours(tokenHourExpTime),
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