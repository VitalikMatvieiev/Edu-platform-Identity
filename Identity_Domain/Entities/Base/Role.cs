using System.Text.Json.Serialization;

namespace Identity_Domain.Entities.Base;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;

    public List<Claim> Claims { get; set; } = new List<Claim>();

    [JsonIgnore]
    public List<Identity> Identities { get; set; } = new List<Identity>();
}