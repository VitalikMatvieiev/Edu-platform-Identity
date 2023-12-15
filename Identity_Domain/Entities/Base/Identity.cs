using Identity_Domain.Entities.Additional;
using System.Text.RegularExpressions;

namespace Identity_Domain.Entities.Base;

public class Identity : Entity
{
    public string Username { get; set; } = string.Empty;

    private string _email = string.Empty;
    public string Email
    {
        get { return _email; }
        set
        {
            if (Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", RegexOptions.IgnoreCase))
                _email = value;
            else throw new Exception("Incorrect Email");
        }
    }

    public string PasswordSalt { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime RegistrationDate { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime LastLogout { get; set; }

    public RefreshToken? Token { get; set; }

    //public List<Claim> Claims { get; set; } = new List<Claim>();

    public List<ClaimIdentity> ClaimIdentities { get; set; } = new List<ClaimIdentity>();

    //public List<Role> Roles { get; set; } = new List<Role>();

    public List<IdentityRole> IdentityRole { get; set; } = new List<IdentityRole>();
}