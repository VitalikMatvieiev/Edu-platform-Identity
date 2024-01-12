using Identity_Domain.Entities.Additional;

namespace Identity_Domain.Entities.Base;

public class Claim : Entity
{
    public string Name { get; set; } = string.Empty;

    public List<ClaimIdentity> ClaimIdentity { get; set; } = new List<ClaimIdentity>();

    public List<ClaimRole> ClaimRole { get; set; } = new List<ClaimRole>();
}