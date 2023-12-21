using Identity_Domain.Entities.Additional;
using System.Text.RegularExpressions;

namespace Identity_Domain.Entities.Base;

public class Identity : Entity
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordSalt { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime RegistrationDate { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime LastLogout { get; set; }

    public RefreshToken? Token { get; set; }

    public List<ClaimIdentity> ClaimIdentities { get; set; } = new List<ClaimIdentity>();

    public List<IdentityRole> IdentityRole { get; set; } = new List<IdentityRole>();
}