using Identity_Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Identity_Domain.Entities.Additional;

public class ClaimIdentity : Entity
{
    public int? ClaimsId { get; set; }

    public Claim? Claims { get; set; }

    [JsonIgnore]
    public int? IdentitiesId { get; set; }

    [JsonIgnore]
    public Identity? Identities { get; set; }
}