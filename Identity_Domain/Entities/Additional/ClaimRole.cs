using Identity_Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Identity_Domain.Entities.Additional;

public class ClaimRole : Entity
{
    public int? ClaimsId { get; set; }

    public Claim? Claims { get; set; }

    [JsonIgnore]
    public int? RolesId { get; set; }

    [JsonIgnore]
    public Role? Roles { get; set; }
}