using Identity_Domain.Entities.Additional;

namespace Identity_Domain.Entities.Base;

public class Role : Entity
{
    public string? Name { get; set; } = string.Empty;

    public List<ClaimRole> ClaimRole { get; set; } = new List<ClaimRole>();

    public List<IdentityRole> IdentityRole { get; set; } = new List<IdentityRole>();
}