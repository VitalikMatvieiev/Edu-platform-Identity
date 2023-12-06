using Identity_Domain.Entities.Base;

namespace Identity_Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; }

    public List<Claim> Claims { get; set; } = new List<Claim>();

    public List<Identity> Identities { get; set; } = new List<Identity>();
}