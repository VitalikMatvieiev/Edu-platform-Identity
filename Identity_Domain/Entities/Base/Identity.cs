namespace Identity_Domain.Entities.Base;

public class Identity : Entity
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public DateTime RegistrationDate { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime LastLogout { get; set; }

    public RefreshToken Token { get; set; }

    public List<Claim> Claims { get; set; } = new List<Claim>();

    public List<Role> Roles { get; set; } = new List<Role>();

    //public bool IsConfirmed { get; set; }    Should we add that ???????
}