using Identity_Domain.Entities.Base;

namespace Identity_Infrastructure.Repositories;

public class IdentityRepository : GenericRepository<Identity>
{
    public IdentityRepository(IdentityDbContext context) : base(context)
    {

    }
}