using System.Text.Json.Serialization;

namespace Identity_Domain.Entities.Base;

public class Claim : Entity
{
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Identity> Identities { get; set; } = new List<Identity>();

    [JsonIgnore]
    public List<Role> Roles { get; set; } = new List<Role>();
}