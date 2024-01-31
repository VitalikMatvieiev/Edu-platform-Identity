namespace Identity_Application.Models.BaseEntitiesModels;

public class IdentityVM
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int?[] RolesIds { get; set; }

    public int?[] ClaimsIds { get; set; }
}