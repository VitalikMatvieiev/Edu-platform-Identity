using Identity_Domain.Entities.Base;

namespace Identity_Infrastructure.Repositories;

public class ClaimRepository : GenericRepository<Claim>
{
    public ClaimRepository(IdentityDbContext context) : base(context)
    {

    }
}