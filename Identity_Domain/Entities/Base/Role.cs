namespace Identity_Domain.Entities.Base;

public class Role : Entity
{
    public string Name { get; set; }

    public List<Claim> Claims { get; set; } = new List<Claim>();

    public List<Identity> Identities { get; set; } = new List<Identity>();
}