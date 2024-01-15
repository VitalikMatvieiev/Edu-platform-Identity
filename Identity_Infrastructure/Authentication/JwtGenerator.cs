using Identity_Application.Interfaces.Authentication;
using Identity_Application.Models.AppSettingsModels;
using Identity_Domain.Entities.Additional;
using Identity_Domain.Entities.Base;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
        try
        {
            if (_config is null)
                throw new Exception("Configuration loading failed");

            var roles = GetRolesJson(identity.IdentityRole
                .Where(x => x.IdentitiesId == identity.Id).ToList());

            var claimsList = GetClaimsJson(identity.IdentityRole
                .Where(x => x.IdentitiesId == identity.Id).ToList(),
                identity.ClaimIdentities.Where(x => x.IdentitiesId == identity.Id).ToList());

            var tokenClaims = new List<System.Security.Claims.Claim>
            {
            new("id", identity.Id.ToString()),
            new ("username", identity.Username),
            new ("email", identity.Email),
            new ("lastLogin", identity.LastLogin.ToString())
            };

            foreach (var role in roles)
                tokenClaims.Add(new("roles", role));

            foreach (var claim in claimsList)
                tokenClaims.Add(new("claims", claim));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey)),
                    SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                issuer: tokenIssuer,
                expires: DateTime.UtcNow.AddHours(tokenHourExpTime),
                claims: tokenClaims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during JWT token generation", ex);
        }
    }

    private List<string> GetRolesJson(List<IdentityRole> roles)
    {
        return roles.Select(role => role.Roles.Name).ToList();
    }

    private List<string> GetClaimsJson(List<IdentityRole> roles, List<ClaimIdentity> claims)
    {
        var claimsToJson = claims.Select(claim => claim.Claims.Name).ToList();

        foreach (var role in roles)
        {
            foreach (var claim in role.Roles.ClaimRole)
            {
                if (!claimsToJson.Contains(claim.Claims.Name))
                    claimsToJson.Add(claim.Claims.Name);
            }
        }

        return claimsToJson;
    }
}