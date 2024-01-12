namespace Identity_Application.Models.BaseEntitiesDTOs;

public class IdentityDTO
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordSalt { get; set; }

    public string PasswordHash { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime LastLogout { get; set; }

    public string Token { get; set; }

    public List<IdentityRoleDTO> Roles { get; set; }

    public List<IdentityClaimDTO> Claims { get; set; }
}

public class IdentityRoleDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class IdentityClaimDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
}