using Identity_Domain.Entities.Additional;
using System.Text.Json.Serialization;

namespace Identity_Domain.Entities.Base;

public class Role : Entity
{
    public string? Name { get; set; } = string.Empty;

    //public List<Claim> Claims { get; set; } = new List<Claim>();

    public List<ClaimRole> ClaimRole { get; set; } = new List<ClaimRole>();

    //[JsonIgnore]
    //public List<Identity> Identities { get; set; } = new List<Identity>();

    [JsonIgnore]
    public List<IdentityRole> IdentityRole { get; set; } = new List<IdentityRole>();
}