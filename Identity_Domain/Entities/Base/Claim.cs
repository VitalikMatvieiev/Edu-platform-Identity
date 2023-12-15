using Identity_Domain.Entities.Additional;
using System.Text.Json.Serialization;

namespace Identity_Domain.Entities.Base;

public class Claim : Entity
{
    public string Name { get; set; } = string.Empty;

    //[JsonIgnore]
    //public List<Identity> Identities { get; set; } = new List<Identity>();

    [JsonIgnore]
    public List<ClaimIdentity> ClaimIdentity { get; set; } = new List<ClaimIdentity>();

    //[JsonIgnore]
    //public List<Role> Roles { get; set; } = new List<Role>();

    [JsonIgnore]
    public List<ClaimRole> ClaimRole { get; set; } = new List<ClaimRole>();
}