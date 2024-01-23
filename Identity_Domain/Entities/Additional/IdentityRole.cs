using Identity_Domain.Entities.Base;

namespace Identity_Domain.Entities.Additional;

public class IdentityRole : Entity
{
    public int? IdentitiesId { get; set; }

    public Identity? Identities { get; set; }

    public int? RolesId { get; set; }

    public Role? Roles { get; set; }
}