namespace Identity_Domain.Entities.Base;

public class Claim : Entity
{
    public string Name { get; set; }

    public List<Identity> Identities { get; set; } = new List<Identity>();

    public List<Role> Roles { get; set; } = new List<Role>();
}